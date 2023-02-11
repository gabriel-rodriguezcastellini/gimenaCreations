using System.ComponentModel.DataAnnotations;

namespace GimenaCreations.Entities;

public class Purchase
{
    public int Id { get; set; }

    [Display(Name = "Supplier"), Required]
    public int SupplierId { get; set; }

    public Supplier Supplier { get; set; }

    [Display(Name = "Invoice number"), Required]
    public string InvoiceNumber { get; set; }

    [Display(Name = "Invoice date")]
    public DateTime InvoiceDate { get; set; }

    [Display(Name = "Invoice expiration date")]
    public DateTime InvoiceExpirationDate { get; set; }

    [Display(Name = "Is paid")]
    public bool IsPaid { get; set; }

    public string Reference { get; set; }
    public ICollection<PurchaseItem> Items { get; set; }

    public decimal GetTotal() => Items.Sum(x => x.Quantity * x.Price);
}
