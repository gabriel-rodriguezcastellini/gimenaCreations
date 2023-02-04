using GimenaCreations.Contracts;
using GimenaCreations.Models.MercadoPago;
using MassTransit;
using MercadoPago.Config;
using System.Text.Json;

namespace GimenaCreations.Consumers;

public sealed class WebhookNotificationConsumer : IConsumer<WebhookNotification>, IDisposable
{
    private readonly IConfiguration _configuration;
    private readonly HttpClient _httpClient = null!;

    public WebhookNotificationConsumer(IConfiguration configuration, HttpClient httpClient)
    {
        _configuration = configuration;
        _httpClient = httpClient;
    }

    public async Task Consume(ConsumeContext<WebhookNotification> context)
    {
        MercadoPagoConfig.AccessToken = _configuration.GetValue<string>("MercadoPago:AccessToken");

        var response = JsonSerializer.Deserialize<GetPaymentResponse>(await 
            (await _httpClient.GetAsync(_configuration.GetValue<string>("MercadoPago:PaymentUrl").Replace("[ID]", context.Message.PaymentId)))
            .Content.ReadAsStringAsync(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        if (response.Status == "approved")
        {
            // TODO update order status
        }

        var a = 0;
    }

    public void Dispose() => _httpClient?.Dispose();
}
