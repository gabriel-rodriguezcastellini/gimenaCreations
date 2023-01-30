namespace GimenaCreations.Models;

public class Order
{
    public int Id { get; set; }
    public Address Address { get; set; } = null!;
    public OrderStatus Status { get; set; }
    public ICollection<CatalogItem> CatalogItems { get; set; } = null!;
    public PaymentMethod PaymentMethod { get; set; }
    public DateTime Date { get; set; }
    public string? Description { get; set; }
    public ApplicationUser ApplicationUser { get; set; } = null!;
    public string ApplicationUserId { get; set; } = null!;
}
