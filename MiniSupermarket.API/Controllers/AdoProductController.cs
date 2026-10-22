using System.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace MiniSupermarket.API.Controllers;

public sealed class ProductLookupDto
{
    public int ProductId { get; set; }
    public string Barcode { get; set; } = string.Empty;
    public string ProductName { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int StockQuantity { get; set; }
}

[Route("api/ado-products")]
[ApiController]
[Authorize]
public sealed class AdoProductController : ControllerBase
{
    private readonly string _connectionString;

    public AdoProductController(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("Thiếu ConnectionStrings:DefaultConnection.");
    }

    // Connected mode: SqlDataReader streams the matching row.
    [HttpGet("barcode/{barcode}")]
    public async Task<IActionResult> GetByBarcode(string barcode, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(barcode))
            return BadRequest(new { message = "Mã vạch không được để trống." });

        const string sql = "SELECT ProductId, Barcode, ProductName, Price, StockQuantity FROM dbo.Products WHERE Barcode = @Barcode;";
        await using var connection = new SqlConnection(_connectionString);
        await using var command = new SqlCommand(sql, connection);
        command.Parameters.Add("@Barcode", SqlDbType.NVarChar, 50).Value = barcode.Trim();

        try
        {
            await connection.OpenAsync(cancellationToken);
            await using var reader = await command.ExecuteReaderAsync(cancellationToken);
            if (!await reader.ReadAsync(cancellationToken))
                return NotFound(new { message = $"Không tìm thấy sản phẩm có mã vạch: {barcode}" });

            return Ok(ReadProduct(reader));
        }
        catch (SqlException)
        {
            return Problem("Không truy vấn được sản phẩm từ SQL Server.", statusCode: StatusCodes.Status500InternalServerError);
        }
    }

    // Disconnected mode: adapter.Fill closes its connection and the response is built from a DataTable.
    [HttpGet("disconnected-cache")]
    public IActionResult GetDisconnectedCache()
    {
        const string sql = "SELECT ProductId, Barcode, ProductName, Price, StockQuantity FROM dbo.Products ORDER BY ProductId;";
        var table = new DataTable("CachedProducts");

        try
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand(sql, connection);
            using var adapter = new SqlDataAdapter(command);
            adapter.Fill(table);
        }
        catch (SqlException)
        {
            return Problem("Không tải được danh sách sản phẩm từ SQL Server.", statusCode: StatusCodes.Status500InternalServerError);
        }

        var rows = table.Rows.Cast<DataRow>().Select(row => new ProductLookupDto
        {
            ProductId = row.Field<int>("ProductId"),
            Barcode = row.Field<string>("Barcode") ?? string.Empty,
            ProductName = row.Field<string>("ProductName") ?? string.Empty,
            Price = row.Field<decimal>("Price"),
            StockQuantity = row.Field<int>("StockQuantity")
        }).ToList();

        return Ok(new { totalLoaded = rows.Count, cachedAt = DateTimeOffset.Now, data = rows });
    }

    [HttpGet("filter-stock")]
    public async Task<IActionResult> FilterStock([FromQuery] int minStock = 50, CancellationToken cancellationToken = default)
    {
        if (minStock < 0)
            return BadRequest(new { message = "minStock phải lớn hơn hoặc bằng 0." });

        const string sql = "SELECT ProductId, Barcode, ProductName, Price, StockQuantity FROM dbo.Products WHERE StockQuantity <= @MinStock ORDER BY StockQuantity, ProductName;";
        var products = new List<ProductLookupDto>();
        await using var connection = new SqlConnection(_connectionString);
        await using var command = new SqlCommand(sql, connection);
        command.Parameters.Add("@MinStock", SqlDbType.Int).Value = minStock;

        try
        {
            await connection.OpenAsync(cancellationToken);
            await using var reader = await command.ExecuteReaderAsync(cancellationToken);
            while (await reader.ReadAsync(cancellationToken))
                products.Add(ReadProduct(reader));
        }
        catch (SqlException)
        {
            return Problem("Không lọc được tồn kho từ SQL Server.", statusCode: StatusCodes.Status500InternalServerError);
        }

        return Ok(products);
    }

    [HttpGet("count")]
    public async Task<IActionResult> Count(CancellationToken cancellationToken)
    {
        await using var connection = new SqlConnection(_connectionString);
        await using var command = new SqlCommand("SELECT COUNT(*) FROM dbo.Products;", connection);
        try
        {
            await connection.OpenAsync(cancellationToken);
            var count = Convert.ToInt32(await command.ExecuteScalarAsync(cancellationToken));
            return Ok(new { totalProducts = count });
        }
        catch (SqlException)
        {
            return Problem("Không đếm được sản phẩm trong SQL Server.", statusCode: StatusCodes.Status500InternalServerError);
        }
    }

    [HttpGet("category-summary")]
    public IActionResult CategorySummary()
    {
        const string sql = "SELECT CategoryId, COUNT(*) AS TotalProducts FROM dbo.Products GROUP BY CategoryId ORDER BY CategoryId;";
        var table = new DataTable("CategorySummary");
        try
        {
            using var connection = new SqlConnection(_connectionString);
            using var adapter = new SqlDataAdapter(sql, connection);
            adapter.Fill(table);
        }
        catch (SqlException)
        {
            return Problem("Không tổng hợp được số sản phẩm theo danh mục.", statusCode: StatusCodes.Status500InternalServerError);
        }

        return Ok(table.Rows.Cast<DataRow>().Select(row => new
        {
            categoryId = row.Field<int>("CategoryId"),
            totalProducts = row.Field<int>("TotalProducts")
        }));
    }

    private static ProductLookupDto ReadProduct(SqlDataReader reader) => new()
    {
        ProductId = reader.GetInt32(reader.GetOrdinal("ProductId")),
        Barcode = reader.IsDBNull(reader.GetOrdinal("Barcode")) ? string.Empty : reader.GetString(reader.GetOrdinal("Barcode")),
        ProductName = reader.GetString(reader.GetOrdinal("ProductName")),
        Price = reader.GetDecimal(reader.GetOrdinal("Price")),
        StockQuantity = reader.GetInt32(reader.GetOrdinal("StockQuantity"))
    };
}
