namespace MiniSupermarket.API.Models;

public sealed class CartItemCheckoutDto
{
    public int ProductId { get; set; }
    public int Quantity { get; set; }

    // Kept for compatibility with POS clients. The server always uses the DB price.
    public decimal UnitPrice { get; set; }
}

public sealed class CheckoutRequestDto
{
    public string? CashierUsername { get; set; }
    public int? CustomerId { get; set; }
    public string? CustomerPhone { get; set; }
    public List<CartItemCheckoutDto> Items { get; set; } = new();
}

public sealed class CashierRevenueDto
{
    public string CashierUsername { get; set; } = string.Empty;
    public int TotalOrders { get; set; }
    public decimal TotalRevenue { get; set; }
    public int TotalPointsAwarded { get; set; }
}
