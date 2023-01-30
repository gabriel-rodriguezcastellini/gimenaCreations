using GimenaCreations.Models;

namespace GimenaCreations.Services;

public interface ICartService
{
    Task<CustomerBasket> GetBasketAsync(string userId);
    Task AddItemToBasketAsync(string userId, int productId);
    Task<CustomerBasket> UpdateBasketAsync(CustomerBasket basket);
    Task CheckoutAsync(CustomerBasket basket);
    Task<CustomerBasket> SetQuantitiesAsync(string userId, Dictionary<string, int> quantities);
    Task DeleteItemAsync(string basketId, int productId);
}
