using GimenaCreations.Contracts;
using GimenaCreations.Data;
using GimenaCreations.Models;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace GimenaCreations.Consumers;

[Authorize]
public sealed class OrderStatusChangedToAwaitingValidationConsumer : IConsumer<OrderStatusChangedToAwaitingValidation>
{
    private readonly ILogger<OrderStatusChangedToAwaitingValidationConsumer> _logger;
    private readonly IHubContext<NotificationHub> hubContext;
    private readonly ApplicationDbContext _context;

    public OrderStatusChangedToAwaitingValidationConsumer(ILogger<OrderStatusChangedToAwaitingValidationConsumer> logger,
        IHubContext<NotificationHub> hubContext, ApplicationDbContext context)
    {
        _logger = logger;
        this.hubContext = hubContext;
        _context = context;
    }

    public async Task Consume(ConsumeContext<OrderStatusChangedToAwaitingValidation> context)
    {
        _logger.LogInformation("Received message: {Message}", JsonSerializer.Serialize(context.Message));
        var order = await _context.Orders.FirstAsync(x => x.Id == context.Message.OrderId && x.ApplicationUserId == context.Message.UserId);

        if (order.PaymentMethod == PaymentMethod.MercadoPago)
        {

        }
    }
}
