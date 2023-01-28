﻿using GimenaCreations.Models;
using StackExchange.Redis;
using System.Text.Json;

namespace GimenaCreations.Services;

public class CartService : ICartService
{
    private readonly IConnectionMultiplexer _redis;
    private readonly ICatalogService _catalogService;
    private readonly ILogger<CartService> _logger;

    public CartService(IConnectionMultiplexer redis, ICatalogService catalogService, ILogger<CartService> logger)
    {
        _redis = redis;
        _catalogService = catalogService;
        _logger = logger;
    }

    public async Task AddItemToBasketAsync(string userId, int productId)
    {
        var item = await _catalogService.GetCatalogItemAsync(productId);
        var basket = await GetBasketAsync(userId);
        var product = basket.Items.SingleOrDefault(i => i.ProductId == productId);

        if (product == null)
        {
            basket.Items.Add(new BasketItem
            {
                ProductId = productId,
                UnitPrice = item!.Price,
                PictureUrl = item.PictureFileName,
                ProductName = item.Name,
                Quantity = 1,
            });
        }
        else
        {
            product.Quantity++;
        }

        await UpdateBasketAsync(basket);
    }

    public Task CheckoutAsync(CustomerBasket basket)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteItemAsync(string basketId, int productId)
    {
        var basket = await _redis.GetDatabase().StringGetAsync(basketId);

        if (basket.IsNullOrEmpty)
        {
            _logger.LogInformation("Problem occur deleting the item.");
        }

        var basketDto = JsonSerializer.Deserialize<CustomerBasket>(basket!, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        basketDto!.Items.Remove(basketDto.Items.First(x => x.ProductId == productId));
        await UpdateBasketAsync(basketDto);
    }

    public async Task<CustomerBasket> GetBasketAsync(string userId)
    {
        var basket = await _redis.GetDatabase().StringGetAsync(userId);

        if (basket.IsNullOrEmpty)
        {
            return new CustomerBasket(userId);
        }

        return JsonSerializer.Deserialize<CustomerBasket>(basket!, new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;
    }

    public Task<Models.Order> GetOrderDraftAsync(string basketId)
    {
        throw new NotImplementedException();
    }

    public async Task<CustomerBasket> SetQuantitiesAsync(string userId, Dictionary<string, int> quantities)
    {
        var basket = await GetBasketAsync(userId);

        foreach (var item in basket.Items)
        {
            item.Quantity = quantities.First(x => x.Key == item.ProductId.ToString()).Value;
        }

        return await UpdateBasketAsync(basket);
    }

    public async Task<CustomerBasket> UpdateBasketAsync(CustomerBasket basket)
    {
        var created = await _redis.GetDatabase().StringSetAsync(basket.BuyerId, JsonSerializer.Serialize(basket));

        if (!created)
        {
            _logger.LogInformation("Problem occur persisting the item.");
            return null!;
        }

        _logger.LogInformation("Basket item persisted succesfully.");
        return await GetBasketAsync(basket.BuyerId);
    }
}
