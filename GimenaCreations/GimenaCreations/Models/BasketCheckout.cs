namespace GimenaCreations.Models;

public class BasketCheckout
{
    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string State { get; set; } = null!;

    public string Country { get; set; } = null!;

    public string ZipCode { get; set; } = null!;

    public PaymentMethod PaymentMethod { get; set; }
}
