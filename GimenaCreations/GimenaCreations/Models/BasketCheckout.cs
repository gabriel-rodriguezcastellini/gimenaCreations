using System.ComponentModel.DataAnnotations;

namespace GimenaCreations.Models;

public class BasketCheckout
{
    public CustomerBasket Basket { get; set; } = null!;

    [Required, Display(Name = "First name")]
    public string FirstName { get; set; } = null!;

    [Required, Display(Name = "Last name")]
    public string LastName { get; set; } = null!;

    [Required]
    public string Street { get; set; } = null!;

    [Required]
    public string City { get; set; } = null!;

    [Required]
    public string State { get; set; } = null!;

    [Required]
    public string Country { get; set; } = null!;

    [Required]
    public string ZipCode { get; set; } = null!;

    [Required]
    public PaymentMethod? PaymentMethod { get; set; }
}
