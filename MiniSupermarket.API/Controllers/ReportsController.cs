using System.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using MiniSupermarket.API.Models;

namespace MiniSupermarket.API.Controllers;

[Route("api/reports")]
[ApiController]
[Authorize(Roles = "Admin")]
public sealed class ReportsController : ControllerBase
{
    private readonly string _connectionString;

    public ReportsController(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("Thiếu ConnectionStrings:DefaultConnection.");
    }

    [HttpGet("revenue-by-cashier")]
    public async Task<IActionResult> GetRevenueByCashier(CancellationToken cancellationToken)
    {
        var reports = new List<CashierRevenueDto>();
        await using var connection = new SqlConnection(_connectionString);
        await using var command = new SqlCommand("dbo.sp_GetRevenueByCashier", connection)
        {
            CommandType = CommandType.StoredProcedure
        };

        try
        {
            await connection.OpenAsync(cancellationToken);
            await using var reader = await command.ExecuteReaderAsync(cancellationToken);
            while (await reader.ReadAsync(cancellationToken))
            {
                reports.Add(new CashierRevenueDto
                {
                    CashierUsername = reader.GetString(reader.GetOrdinal("CashierUsername")),
                    TotalOrders = reader.GetInt32(reader.GetOrdinal("TotalOrders")),
                    TotalRevenue = reader.GetDecimal(reader.GetOrdinal("TotalRevenue")),
                    TotalPointsAwarded = reader.GetInt32(reader.GetOrdinal("TotalPointsAwarded"))
                });
            }
        }
        catch (SqlException ex) when (ex.Number == 2812)
        {
            return Problem(
                "Stored Procedure dbo.sp_GetRevenueByCashier chưa có trong database TiemtaphoaNgocMai. Chạy file SqlScripts/Buoi5.sql trong SSMS.",
                statusCode: StatusCodes.Status503ServiceUnavailable);
        }
        catch (SqlException)
        {
            return Problem("Không truy vấn được báo cáo doanh thu từ SQL Server.", statusCode: StatusCodes.Status500InternalServerError);
        }

        return Ok(new
        {
            success = true,
            generatedAt = DateTimeOffset.Now,
            totalCashiers = reports.Count,
            grandTotalRevenue = reports.Sum(report => report.TotalRevenue),
            data = reports
        });
    }
}
