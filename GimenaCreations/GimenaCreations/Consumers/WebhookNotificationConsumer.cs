﻿using GimenaCreations.Contracts;
using GimenaCreations.Extensions;
using GimenaCreations.Models;
using GimenaCreations.Models.MercadoPago;
using GimenaCreations.Services;
using MassTransit;
using MercadoPago.Config;
using Microsoft.AspNetCore.SignalR;
using System.Text.Json;

namespace GimenaCreations.Consumers;

public sealed class WebhookNotificationConsumer : IConsumer<WebhookNotification>, IDisposable
{
    private readonly IConfiguration _configuration;
    private readonly HttpClient _httpClient;
    private readonly ILogger<WebhookNotificationConsumer> _logger;    
    private readonly IOrderService _orderService;
    private readonly IHubContext<NotificationHub> hubContext;

    public WebhookNotificationConsumer(IConfiguration configuration, HttpClient httpClient,
        ILogger<WebhookNotificationConsumer> logger, IOrderService orderService, IHubContext<NotificationHub> hubContext)
    {
        _configuration = configuration;
        _httpClient = httpClient;
        _logger = logger;
        _orderService = orderService;
        this.hubContext = hubContext;
    }

    public async Task Consume(ConsumeContext<WebhookNotification> context)
    {
        _logger.LogInformation("Received message: {Message}", JsonSerializer.Serialize(context.Message));
        MercadoPagoConfig.AccessToken = _configuration.GetValue<string>("MercadoPago:AccessToken");        

        var response = JsonSerializer.Deserialize<GetPaymentResponse>(await
            (await _httpClient.GetAsync(_configuration.GetValue<string>("MercadoPago:PaymentUrl").Replace("[ID]", context.Message.PaymentId)))
            .Content.ReadAsStringAsync(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        var order = await _orderService.GetOrderByIdAsync(Convert.ToInt32(response!.ExternalReference));

        switch (response!.Status)
        {
            case Status.Approved:
                await _orderService.UpdateOrderStatusAsync(order.Id, OrderStatus.Paid);
                await hubContext.Clients.User(order.ApplicationUserId).SendAsync("UpdatedOrderState", new { order.Id, status = OrderStatus.Paid.GetDisplayName() });
                break;
            case Status.Cancelled:
            case Status.Refunded:
            case Status.ChargedBack:            
                await _orderService.UpdateOrderStatusAsync(Convert.ToInt32(order.Id), OrderStatus.Cancelled);
                await hubContext.Clients.User(order.ApplicationUserId).SendAsync("UpdatedOrderState", new { order.Id, status = OrderStatus.Cancelled.GetDisplayName() });
                break;
            default:
                break;
        }
    }

    public void Dispose() => _httpClient?.Dispose();
}
