using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using MiniSupermarket.API.Data;
using MiniSupermarket.API.Models;

namespace MiniSupermarket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin,Cashier")]
    public class OrdersController : ControllerBase
    {
        private readonly SupermarketDbContext _context;

        public OrdersController(SupermarketDbContext context)
        {
            _context = context;
        }

        // POST /api/orders/checkout: Xử lý thanh toán đơn hàng tại quầy POS
        [HttpPost("checkout")]
        public async Task<IActionResult> Checkout([FromBody] CheckoutRequest request)
        {
            if (request?.Items == null || request.Items.Count == 0 ||
                request.Items.Any(item => item.ProductId <= 0 || item.Quantity <= 0))
            {
                return BadRequest(new { message = "Giỏ hàng rỗng hoặc có số lượng không hợp lệ." });
            }

            var quantities = request.Items
                .GroupBy(item => item.ProductId)
                .ToDictionary(group => group.Key, group => group.Sum(item => item.Quantity));
            var products = await _context.Products
                .Where(product => quantities.Keys.Contains(product.ProductId))
                .ToDictionaryAsync(product => product.ProductId);

            if (products.Count != quantities.Count)
            {
                return NotFound(new { message = "Một hoặc nhiều sản phẩm không còn tồn tại." });
            }

            foreach (var (productId, quantity) in quantities)
            {
                if (products[productId].StockQuantity < quantity)
                {
                    return BadRequest(new { message = $"Sản phẩm '{products[productId].ProductName}' không đủ tồn kho." });
                }
            }

            var customerPhone = string.IsNullOrWhiteSpace(request.CustomerPhone)
                ? null
                : request.CustomerPhone.Trim();
            var customer = customerPhone == null
                ? null
                : await _context.Customers.FirstOrDefaultAsync(c => c.PhoneNumber == customerPhone);
            if (customerPhone != null && customer == null)
            {
                return BadRequest(new { message = "Không tìm thấy khách hàng theo số điện thoại." });
            }

            var details = quantities.Select(pair =>
            {
                var product = products[pair.Key];
                return new OrderDetail
                {
                    ProductId = product.ProductId,
                    ProductName = product.ProductName,
                    UnitPrice = product.Price,
                    Quantity = pair.Value,
                    LineTotal = product.Price * pair.Value
                };
            }).ToList();
            var total = details.Sum(detail => detail.LineTotal);
            var order = new Order
            {
                CreatedAt = DateTime.Now,
                CashierUsername = User.Identity?.Name ?? string.Empty,
                CustomerPhone = customerPhone,
                TotalAmount = total,
                RewardPointsAwarded = customer == null ? 0 : (int)(total / 10000m),
                Details = details
            };

            await using var transaction = await _context.Database.BeginTransactionAsync();
            _context.Orders.Add(order);
            foreach (var (productId, quantity) in quantities)
            {
                products[productId].StockQuantity -= quantity;
            }

            if (customer != null)
            {
                customer.RewardPoints += (int)(total / 10000m);
            }

            await _context.SaveChangesAsync();
            await transaction.CommitAsync();

            return Ok(new
            {
                message = "Thanh toán thành công và đã lưu hóa đơn.",
                orderId = order.OrderId,
                totalAmount = order.TotalAmount
            });
        }

        [HttpGet("report")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetDailyReport([FromQuery] DateTime date)
        {
            var start = date.Date;
            var end = start.AddDays(1);
            var orders = _context.Orders.AsNoTracking()
                .Where(order => order.CreatedAt >= start && order.CreatedAt < end);

            var orderCount = await orders.CountAsync();
            var revenue = await orders.Select(order => (decimal?)order.TotalAmount).SumAsync() ?? 0m;
            var bestSeller = await (
                from detail in _context.OrderDetails.AsNoTracking()
                join order in _context.Orders.AsNoTracking() on detail.OrderId equals order.OrderId
                where order.CreatedAt >= start && order.CreatedAt < end
                group detail by detail.ProductName into groupByProduct
                orderby groupByProduct.Sum(detail => detail.Quantity) descending
                select new
                {
                    ProductName = groupByProduct.Key,
                    Quantity = groupByProduct.Sum(detail => detail.Quantity)
                })
                .FirstOrDefaultAsync();

            return Ok(new
            {
                orderCount,
                totalRevenue = revenue,
                bestSeller = bestSeller == null
                    ? "Chưa có dữ liệu bán hàng trong ngày này"
                    : $"{bestSeller.ProductName} ({bestSeller.Quantity} sản phẩm)"
            });
        }
    }

    public class CheckoutRequest
    {
        public string CashierUsername { get; set; } = string.Empty;
        public string? CustomerPhone { get; set; }
        public List<CheckoutItem> Items { get; set; } = new();
    }

    public class CheckoutItem
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
