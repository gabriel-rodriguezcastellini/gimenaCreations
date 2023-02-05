using System.ComponentModel.DataAnnotations;

namespace GimenaCreations.Models;

public class CustomerBasket
{
    [Required]
    public string BuyerId { get; set; }

    public List<BasketItem> Items { get; set; } = new();

    public CustomerBasket()
    {

    }

    public CustomerBasket(string customerId) => BuyerId = customerId;

    public decimal Total() => Math.Round(Items.Sum(x => x.UnitPrice * x.Quantity), 2);
}
