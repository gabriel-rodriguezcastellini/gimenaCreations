using GimenaCreations.Models;

namespace GimenaCreations.Contracts;

public record OrderStatusChangedToSubmitted
{    
    public int OrderId { get; }
    public OrderStatus OrderStatus { get; }
    public string UserId { get; }
    public PaymentMethod PaymentMethod { get; }

    public OrderStatusChangedToSubmitted(int orderId, OrderStatus orderStatus, string userId, PaymentMethod paymentMethod)
    {
        OrderId = orderId;
        OrderStatus = orderStatus;
        UserId = userId;
        PaymentMethod = paymentMethod;
    }
}
