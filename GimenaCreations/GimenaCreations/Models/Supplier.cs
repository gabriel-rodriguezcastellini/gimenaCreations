using System.ComponentModel.DataAnnotations;

namespace GimenaCreations.Models;

public class Supplier
{
    public int Id { get; set; }
    public string Name { get; set; }

    [Display(Name = "Company address")]
    public string CompanyAddress { get; set; }

    public string Cuit { get; set; }

    [Display(Name = "AFIP responsibility")]
    public string AfipResponsibility { get; set; }

    public string Phone { get; set; }

    [EmailAddress]
    public string Email { get; set; }

    public string Website { get; set; }
    public ICollection<Purchase> Purchases { get; set; }
}
