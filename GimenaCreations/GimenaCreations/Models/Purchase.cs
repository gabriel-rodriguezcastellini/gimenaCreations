namespace GimenaCreations.Models;

public class Purchase
{
    public int Id { get; set; }
    public int SupplierId { get; set; }
    public Supplier Supplier { get; set; }
    public string InvoiceNumber { get; set; }
    public DateTime InvoiceDate { get; set; }
    public DateTime InvoiceExpirationDate { get; set;}
    public bool IsPaid { get; set; }
    public string Reference { get; set; }
    public ICollection<PurchaseItem> Items { get; set; }
}
