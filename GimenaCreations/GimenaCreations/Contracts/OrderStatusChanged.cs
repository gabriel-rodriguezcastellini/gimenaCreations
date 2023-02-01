using GimenaCreations.Models;

namespace GimenaCreations.Contracts;

public record OrderStatusChanged
{    
    public int OrderId { get; }
    public OrderStatus OrderStatus { get; }
    public string UserId { get; }

    public OrderStatusChanged(int orderId, OrderStatus orderStatus, string userId)
    {
        OrderId = orderId;
        OrderStatus = orderStatus;
        UserId = userId;
    }
}
