using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiniSupermarket.API.Models;

[Table("Orders")]
public sealed class Order
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int OrderId { get; set; }

    public DateTime CreatedAt { get; set; }

    [Required]
    [StringLength(50)]
    public string CashierUsername { get; set; } = string.Empty;

    [StringLength(15)]
    public string? CustomerPhone { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal TotalAmount { get; set; }

    public int RewardPointsAwarded { get; set; }

    public ICollection<OrderDetail> Details { get; set; } = new List<OrderDetail>();
}
