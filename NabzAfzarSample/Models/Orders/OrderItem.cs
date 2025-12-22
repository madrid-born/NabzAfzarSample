using System.ComponentModel.DataAnnotations;

namespace NabzAfzarSample.Models.Orders;

public class OrderItem
{
    public int Id { get; set; }

    public int OrderId { get; set; }
    public Order Order { get; set; } = null!;

    public int ProductId { get; set; }

    [Required]
    public string ProductName { get; set; } = string.Empty;

    public decimal UnitPrice { get; set; }

    public int Quantity { get; set; }

    public decimal Total => UnitPrice * Quantity;
}