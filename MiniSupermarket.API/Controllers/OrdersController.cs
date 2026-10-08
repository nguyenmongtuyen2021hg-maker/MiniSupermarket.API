using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniSupermarket.API.Data;

namespace MiniSupermarket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
            if (request == null || request.Items == null || !request.Items.Any())
            {
                return BadRequest(new { message = "Giỏ hàng rỗng, không thể thanh toán!" });
            }

            // Trừ tồn kho sản phẩm
            foreach (var item in request.Items)
            {
                var prod = await _context.Products.FindAsync(item.ProductId);
                if (prod != null)
                {
                    prod.StockQuantity = Math.Max(0, prod.StockQuantity - item.Quantity);
                }
            }

            // Tích điểm nếu có khách hàng
            if (!string.IsNullOrWhiteSpace(request.CustomerPhone))
            {
                var customer = await _context.Customers.FirstOrDefaultAsync(c => c.PhoneNumber == request.CustomerPhone.Trim());
                if (customer != null)
                {
                    decimal total = request.Items.Sum(x => x.UnitPrice * x.Quantity);
                    int addedPoints = (int)(total / 10000); // 10.000đ = 1 điểm
                    customer.RewardPoints += addedPoints;
                }
            }

            await _context.SaveChangesAsync();

            return Ok(new
            {
                message = "Thanh toán thành công và đã in hóa đơn!",
                orderId = Guid.NewGuid().ToString().Substring(0, 8).ToUpper(),
                totalAmount = request.Items.Sum(x => x.UnitPrice * x.Quantity)
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
