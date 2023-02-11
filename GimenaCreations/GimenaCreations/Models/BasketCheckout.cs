using System.ComponentModel.DataAnnotations;
using GimenaCreations.Entities;

namespace GimenaCreations.Models;

public class BasketCheckout
{
    public CustomerBasket Basket { get; set; }

    [Required, Display(Name = "First name")]
    public string FirstName { get; set; }

    [Required, Display(Name = "Last name")]
    public string LastName { get; set; }

    [Required]
    public string Street { get; set; }

    [Required]
    public string City { get; set; }

    [Required]
    public string State { get; set; }

    [Required]
    public string Country { get; set; }

    [Required]
    public string ZipCode { get; set; }

    [Required]
    public PaymentMethod? PaymentMethod { get; set; }
}
