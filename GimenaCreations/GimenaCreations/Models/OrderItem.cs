namespace GimenaCreations.Models;

public class OrderItem
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public Order Order { get; set; } = null!;
    public int CatalogItemId { get; set; }
    public CatalogItem CatalogItem { get; set; } = null!;
    public string ProductName { get; set; } = null!;
    public string PictureUrl { get; set; } = null!;
    public decimal UnitPrice { get; set; }
    public int Units { get; set; }
}
