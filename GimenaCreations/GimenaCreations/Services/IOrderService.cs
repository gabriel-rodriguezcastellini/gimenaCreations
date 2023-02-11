using GimenaCreations.Entities;

namespace GimenaCreations.Services;

public interface IOrderService
{
    Task CreateOrderAsync(Order order);
    Task<List<Order>> GetAllOrdersAsync(string userId);
    Task<Order> GetOrderByIdAsync(int id, string userId);
    Task<Order> GetOrderByIdAsync(int id);
    Task UpdateOrderStatusAsync(int id, OrderStatus orderStatus, string description = null);
}
