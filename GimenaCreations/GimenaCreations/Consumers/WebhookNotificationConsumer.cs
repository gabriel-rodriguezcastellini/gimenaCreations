using GimenaCreations.Contracts;
using GimenaCreations.Data;
using GimenaCreations.Models.MercadoPago;
using MassTransit;
using MercadoPago.Config;
using System.Text.Json;

namespace GimenaCreations.Consumers;

public sealed class WebhookNotificationConsumer : IConsumer<WebhookNotification>, IDisposable
{
    private readonly IConfiguration _configuration;
    private readonly HttpClient _httpClient;
    private readonly ILogger<WebhookNotificationConsumer> _logger;
    private readonly ApplicationDbContext _applicationDbContext;

    public WebhookNotificationConsumer(IConfiguration configuration, HttpClient httpClient, ILogger<WebhookNotificationConsumer> logger, ApplicationDbContext applicationDbContext)
    {
        _configuration = configuration;
        _httpClient = httpClient;
        _logger = logger;
        _applicationDbContext = applicationDbContext;
    }

    public async Task Consume(ConsumeContext<WebhookNotification> context)
    {
        _logger.LogInformation("Received message: {Message}", JsonSerializer.Serialize(context.Message));
        MercadoPagoConfig.AccessToken = _configuration.GetValue<string>("MercadoPago:AccessToken");

        var response = JsonSerializer.Deserialize<GetPaymentResponse>(await 
            (await _httpClient.GetAsync(_configuration.GetValue<string>("MercadoPago:PaymentUrl").Replace("[ID]", context.Message.PaymentId)))
            .Content.ReadAsStringAsync(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        if (response.Status == "approved")
        {
            
        }

        var a = 0;
    }

    public void Dispose() => _httpClient?.Dispose();
}
