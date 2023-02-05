using System.ComponentModel.DataAnnotations;

namespace GimenaCreations.Models;

public class OrderItem
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public Order Order { get; set; }
    public int CatalogItemId { get; set; }
    public CatalogItem CatalogItem { get; set; }

    [Display(Name ="Product name")]
    public string ProductName { get; set; }

    [Display(Name = "Picture")]
    public string PictureUrl { get; set; }

    [Display(Name = "Unit price")]
    public decimal UnitPrice { get; set; }

    public int Units { get; set; }
    public bool RequireFile { get; set; }
    public ICollection<File> Files { get; set; }
}
