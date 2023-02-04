namespace GimenaCreations.Contracts;

public record OrderStatusChangedToAwaitingValidation
{
    public int OrderId { get; }
    public string UserId { get; }

    public OrderStatusChangedToAwaitingValidation(int orderId, string userId)
    {
        OrderId = orderId;
        UserId = userId;
    }
}
