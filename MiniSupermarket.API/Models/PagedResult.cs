namespace MiniSupermarket.API.Models
{
    public sealed class PagedResult<T>
    {
        public int CurrentPage { get; init; }
        public int PageSize { get; init; }
        public int TotalItems { get; init; }
        public int TotalPages { get; init; }
        public List<T> Items { get; init; } = new();
    }

    public sealed class ProductItemDto
    {
        public int ProductId { get; init; }
        public string Barcode { get; init; } = string.Empty;
        public string ProductName { get; init; } = string.Empty;
        public decimal Price { get; init; }
        public int StockQuantity { get; init; }
        public int CategoryId { get; init; }
        public string CategoryName { get; init; } = string.Empty;
    }

    public sealed class CustomerItemDto
    {
        public int CustomerId { get; init; }
        public string CustomerName { get; init; } = string.Empty;
        public string PhoneNumber { get; init; } = string.Empty;
        public string? Address { get; init; }
        public int RewardPoints { get; init; }
        public string MembershipRank { get; init; } = string.Empty;
    }
}
