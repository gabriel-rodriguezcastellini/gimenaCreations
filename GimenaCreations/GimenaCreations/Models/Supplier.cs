using System.ComponentModel.DataAnnotations;

namespace GimenaCreations.Models;

public class Supplier
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string CompanyAddress { get; set; }
    public string Cuit { get; set; }
    public string AfipResponsibility { get; set; }
    public string Phone { get; set; }

    [EmailAddress]
    public string Email { get; set; }

    public string WebSite { get; set; }
    public ICollection<Purchase> Purchases { get; set; }
}
