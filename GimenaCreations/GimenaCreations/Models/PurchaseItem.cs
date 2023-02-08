using System.ComponentModel.DataAnnotations;

namespace GimenaCreations.Models;

public class PurchaseItem
{
    public int Id { get; set; }
    public int Quantity { get; set; }

    [Display(Name = "Catalog item")]
    public int CatalogItemId { get; set; }

    public CatalogItem CatalogItem { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int PurchaseId { get; set; }
    public Purchase Purchase { get; set; }
}
