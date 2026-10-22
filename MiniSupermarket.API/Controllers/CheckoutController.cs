using System.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using MiniSupermarket.API.Models;

namespace MiniSupermarket.API.Controllers;

[Route("api/checkout")]
[ApiController]
[Authorize(Roles = "Admin,Cashier")]
public sealed class CheckoutController : ControllerBase
{
    private readonly string _connectionString;

    public CheckoutController(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("Thiếu ConnectionStrings:DefaultConnection.");
    }

    [HttpPost]
    public IActionResult ProcessCheckout([FromBody] CheckoutRequestDto request)
    {
        var username = User.Identity?.Name;
        if (string.IsNullOrWhiteSpace(username))
            return Unauthorized();
        if (!string.IsNullOrWhiteSpace(request.CashierUsername) &&
            !string.Equals(request.CashierUsername.Trim(), username, StringComparison.OrdinalIgnoreCase))
        {
            return Forbid();
        }
        if (request.Items == null || request.Items.Count == 0 ||
            request.Items.Any(item => item.ProductId <= 0 || item.Quantity <= 0))
        {
            return BadRequest(new { success = false, message = "Giỏ hàng rỗng hoặc có mã/số lượng không hợp lệ." });
        }

        var quantities = request.Items
            .GroupBy(item => item.ProductId)
            .ToDictionary(group => group.Key, group => group.Sum(item => item.Quantity));

        using var connection = new SqlConnection(_connectionString);
        connection.Open();
        using var transaction = connection.BeginTransaction(IsolationLevel.Serializable);

        try
        {
            // Lock products until commit/rollback so two checkout requests cannot sell the same stock.
            var products = new Dictionary<int, CheckoutProduct>();
            foreach (var (productId, quantity) in quantities.OrderBy(pair => pair.Key))
            {
                const string stockSql = @"
SELECT ProductId, ProductName, Price, StockQuantity
FROM dbo.Products WITH (UPDLOCK, ROWLOCK)
WHERE ProductId = @ProductId;";
                using var stockCommand = new SqlCommand(stockSql, connection, transaction);
                stockCommand.Parameters.Add("@ProductId", SqlDbType.Int).Value = productId;
                using var reader = stockCommand.ExecuteReader();
                if (!reader.Read())
                    throw new CheckoutRejectedException($"Không tìm thấy sản phẩm có mã {productId}.");

                var product = new CheckoutProduct(
                    reader.GetInt32(reader.GetOrdinal("ProductId")),
                    reader.GetString(reader.GetOrdinal("ProductName")),
                    reader.GetDecimal(reader.GetOrdinal("Price")),
                    reader.GetInt32(reader.GetOrdinal("StockQuantity")));
                products.Add(productId, product);
            }

            int? customerId = null;
            string? customerPhone = string.IsNullOrWhiteSpace(request.CustomerPhone) ? null : request.CustomerPhone.Trim();
            if (request.CustomerId is > 0)
            {
                using var customerCommand = new SqlCommand(
                    "SELECT CustomerId, PhoneNumber FROM dbo.Customers WITH (UPDLOCK, ROWLOCK) WHERE CustomerId = @CustomerId;",
                    connection, transaction);
                customerCommand.Parameters.Add("@CustomerId", SqlDbType.Int).Value = request.CustomerId.Value;
                using var reader = customerCommand.ExecuteReader();
                if (!reader.Read())
                    throw new CheckoutRejectedException("Không tìm thấy khách hàng đã chọn.");
                customerId = reader.GetInt32(reader.GetOrdinal("CustomerId"));
                customerPhone = reader.IsDBNull(reader.GetOrdinal("PhoneNumber"))
                    ? null
                    : reader.GetString(reader.GetOrdinal("PhoneNumber"));
            }
            else if (customerPhone != null)
            {
                using var customerCommand = new SqlCommand(
                    "SELECT CustomerId FROM dbo.Customers WITH (UPDLOCK, ROWLOCK) WHERE PhoneNumber = @PhoneNumber;",
                    connection, transaction);
                customerCommand.Parameters.Add("@PhoneNumber", SqlDbType.NVarChar, 20).Value = customerPhone;
                var result = customerCommand.ExecuteScalar();
                if (result == null || result == DBNull.Value)
                    throw new CheckoutRejectedException("Không tìm thấy khách hàng theo số điện thoại.");
                customerId = Convert.ToInt32(result);
            }

            var totalItems = quantities.Values.Sum();
            var totalAmount = quantities.Sum(pair => products[pair.Key].Price * pair.Value);
            var pointsAwarded = (int)(totalAmount / 10000m);

            const string insertOrderSql = @"
INSERT INTO dbo.Orders (CreatedAt, CashierUsername, CustomerPhone, TotalAmount, RewardPointsAwarded)
OUTPUT INSERTED.OrderId
VALUES (@CreatedAt, @CashierUsername, @CustomerPhone, @TotalAmount, @RewardPointsAwarded);";
            int orderId;
            using (var orderCommand = new SqlCommand(insertOrderSql, connection, transaction))
            {
                orderCommand.Parameters.Add("@CreatedAt", SqlDbType.DateTime2).Value = DateTime.Now;
                orderCommand.Parameters.Add("@CashierUsername", SqlDbType.NVarChar, 100).Value = username;
                orderCommand.Parameters.Add("@CustomerPhone", SqlDbType.NVarChar, 20).Value = (object?)customerPhone ?? DBNull.Value;
                var amountParameter = orderCommand.Parameters.Add("@TotalAmount", SqlDbType.Decimal);
                amountParameter.Precision = 18;
                amountParameter.Scale = 2;
                amountParameter.Value = totalAmount;
                orderCommand.Parameters.Add("@RewardPointsAwarded", SqlDbType.Int).Value = pointsAwarded;
                orderId = Convert.ToInt32(orderCommand.ExecuteScalar());
            }

            foreach (var (productId, quantity) in quantities.OrderBy(pair => pair.Key))
            {
                var product = products[productId];
                if (product.StockQuantity < quantity)
                    throw new CheckoutRejectedException(
                        $"Sản phẩm '{product.ProductName}' không đủ tồn kho (còn {product.StockQuantity}, yêu cầu {quantity}).");

                const string detailSql = @"
INSERT INTO dbo.OrderDetails (OrderId, ProductId, ProductName, UnitPrice, Quantity, LineTotal)
VALUES (@OrderId, @ProductId, @ProductName, @UnitPrice, @Quantity, @LineTotal);";
                using (var detailCommand = new SqlCommand(detailSql, connection, transaction))
                {
                    detailCommand.Parameters.Add("@OrderId", SqlDbType.Int).Value = orderId;
                    detailCommand.Parameters.Add("@ProductId", SqlDbType.Int).Value = productId;
                    detailCommand.Parameters.Add("@ProductName", SqlDbType.NVarChar, 150).Value = product.ProductName;
                    AddMoneyParameter(detailCommand, "@UnitPrice", product.Price);
                    detailCommand.Parameters.Add("@Quantity", SqlDbType.Int).Value = quantity;
                    AddMoneyParameter(detailCommand, "@LineTotal", product.Price * quantity);
                    detailCommand.ExecuteNonQuery();
                }

                using var stockCommand = new SqlCommand(
                    "UPDATE dbo.Products SET StockQuantity = StockQuantity - @Quantity WHERE ProductId = @ProductId AND StockQuantity >= @Quantity;",
                    connection, transaction);
                stockCommand.Parameters.Add("@Quantity", SqlDbType.Int).Value = quantity;
                stockCommand.Parameters.Add("@ProductId", SqlDbType.Int).Value = productId;
                if (stockCommand.ExecuteNonQuery() != 1)
                    throw new CheckoutRejectedException($"Tồn kho sản phẩm '{product.ProductName}' vừa thay đổi. Vui lòng tải lại giỏ hàng.");
            }

            if (customerId.HasValue && pointsAwarded > 0)
            {
                using var customerCommand = new SqlCommand(
                    "UPDATE dbo.Customers SET RewardPoints = COALESCE(RewardPoints, 0) + @Points WHERE CustomerId = @CustomerId;",
                    connection, transaction);
                customerCommand.Parameters.Add("@Points", SqlDbType.Int).Value = pointsAwarded;
                customerCommand.Parameters.Add("@CustomerId", SqlDbType.Int).Value = customerId.Value;
                customerCommand.ExecuteNonQuery();
            }

            transaction.Commit();
            return Ok(new
            {
                success = true,
                message = "Thanh toán thành công và đã lưu hóa đơn.",
                orderId,
                orderCode = $"HD-{DateTime.Now:yyyyMMdd}-{orderId:D6}",
                totalItems,
                finalAmount = totalAmount,
                rewardPointsAdded = customerId.HasValue ? pointsAwarded : 0
            });
        }
        catch (CheckoutRejectedException ex)
        {
            TryRollback(transaction);
            return BadRequest(new { success = false, message = "Thanh toán bị hủy, dữ liệu đã được hoàn tác.", errorDetail = ex.Message });
        }
        catch (SqlException ex)
        {
            TryRollback(transaction);
            return Problem($"Lỗi SQL trong giao dịch checkout; giao dịch đã rollback. {ex.Message}", statusCode: StatusCodes.Status500InternalServerError);
        }
        catch (Exception ex)
        {
            TryRollback(transaction);
            return Problem($"Checkout thất bại; giao dịch đã rollback. {ex.Message}", statusCode: StatusCodes.Status500InternalServerError);
        }
    }

    [HttpDelete("cancel/{orderId:int}")]
    [Authorize(Roles = "Admin")]
    public IActionResult CancelOrder(int orderId)
    {
        if (orderId <= 0)
            return BadRequest(new { message = "Mã hóa đơn không hợp lệ." });

        using var connection = new SqlConnection(_connectionString);
        connection.Open();
        using var transaction = connection.BeginTransaction(IsolationLevel.Serializable);
        try
        {
            string? customerPhone;
            int pointsAwarded;
            using (var orderCommand = new SqlCommand(
                "SELECT CustomerPhone, RewardPointsAwarded FROM dbo.Orders WITH (UPDLOCK, ROWLOCK) WHERE OrderId = @OrderId;",
                connection, transaction))
            {
                orderCommand.Parameters.Add("@OrderId", SqlDbType.Int).Value = orderId;
                using var reader = orderCommand.ExecuteReader();
                if (!reader.Read())
                    throw new CheckoutRejectedException($"Không tìm thấy hóa đơn {orderId}.");
                customerPhone = reader.IsDBNull(reader.GetOrdinal("CustomerPhone"))
                    ? null
                    : reader.GetString(reader.GetOrdinal("CustomerPhone"));
                pointsAwarded = reader.GetInt32(reader.GetOrdinal("RewardPointsAwarded"));
            }

            var items = new List<(int ProductId, int Quantity)>();
            using (var detailsCommand = new SqlCommand(
                "SELECT ProductId, Quantity FROM dbo.OrderDetails WHERE OrderId = @OrderId;",
                connection, transaction))
            {
                detailsCommand.Parameters.Add("@OrderId", SqlDbType.Int).Value = orderId;
                using var reader = detailsCommand.ExecuteReader();
                while (reader.Read())
                    items.Add((reader.GetInt32(0), reader.GetInt32(1)));
            }
            if (items.Count == 0)
                throw new CheckoutRejectedException("Hóa đơn không có chi tiết hàng để hoàn kho.");

            foreach (var item in items)
            {
                using var restoreCommand = new SqlCommand(
                    "UPDATE dbo.Products SET StockQuantity = StockQuantity + @Quantity WHERE ProductId = @ProductId;",
                    connection, transaction);
                restoreCommand.Parameters.Add("@Quantity", SqlDbType.Int).Value = item.Quantity;
                restoreCommand.Parameters.Add("@ProductId", SqlDbType.Int).Value = item.ProductId;
                if (restoreCommand.ExecuteNonQuery() != 1)
                    throw new CheckoutRejectedException($"Không thể hoàn kho sản phẩm {item.ProductId}.");
            }

            if (customerPhone != null && pointsAwarded > 0)
            {
                using var pointsCommand = new SqlCommand(@"
UPDATE dbo.Customers
SET RewardPoints = CASE WHEN COALESCE(RewardPoints, 0) >= @Points
                        THEN COALESCE(RewardPoints, 0) - @Points ELSE 0 END
WHERE PhoneNumber = @PhoneNumber;", connection, transaction);
                pointsCommand.Parameters.Add("@Points", SqlDbType.Int).Value = pointsAwarded;
                pointsCommand.Parameters.Add("@PhoneNumber", SqlDbType.NVarChar, 20).Value = customerPhone;
                pointsCommand.ExecuteNonQuery();
            }

            using (var deleteDetails = new SqlCommand("DELETE FROM dbo.OrderDetails WHERE OrderId = @OrderId;", connection, transaction))
            {
                deleteDetails.Parameters.Add("@OrderId", SqlDbType.Int).Value = orderId;
                deleteDetails.ExecuteNonQuery();
            }
            using (var deleteOrder = new SqlCommand("DELETE FROM dbo.Orders WHERE OrderId = @OrderId;", connection, transaction))
            {
                deleteOrder.Parameters.Add("@OrderId", SqlDbType.Int).Value = orderId;
                if (deleteOrder.ExecuteNonQuery() != 1)
                    throw new CheckoutRejectedException("Không xóa được hóa đơn; dữ liệu sẽ được khôi phục.");
            }

            transaction.Commit();
            return Ok(new { success = true, message = $"Đã hủy hóa đơn {orderId}, hoàn kho và điều chỉnh điểm thưởng." });
        }
        catch (CheckoutRejectedException ex)
        {
            TryRollback(transaction);
            return NotFound(new { success = false, message = ex.Message, rolledBack = true });
        }
        catch (Exception ex)
        {
            TryRollback(transaction);
            return Problem($"Hủy hóa đơn thất bại; giao dịch đã rollback. {ex.Message}", statusCode: StatusCodes.Status500InternalServerError);
        }
    }

    private static void AddMoneyParameter(SqlCommand command, string name, decimal value)
    {
        var parameter = command.Parameters.Add(name, SqlDbType.Decimal);
        parameter.Precision = 18;
        parameter.Scale = 2;
        parameter.Value = value;
    }

    private static void TryRollback(SqlTransaction transaction)
    {
        try { transaction.Rollback(); }
        catch (InvalidOperationException) { }
        catch (SqlException) { }
    }

    private sealed record CheckoutProduct(int ProductId, string ProductName, decimal Price, int StockQuantity);
    private sealed class CheckoutRejectedException(string message) : Exception(message);
}
