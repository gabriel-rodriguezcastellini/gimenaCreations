using GimenaCreations.Contracts;
using GimenaCreations.Data;
using GimenaCreations.Models;
using GimenaCreations.Settings;
using MassTransit;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace GimenaCreations.BackgroundTasks;

public class GracePeriodManagerService : BackgroundService
{
    private readonly ILogger<GracePeriodManagerService> logger;
    private readonly IBus bus;
    private readonly BackgroundTask backgroundTask;
    private readonly IServiceScopeFactory serviceScopeFactory;

    public GracePeriodManagerService(ILogger<GracePeriodManagerService> logger, IBus bus, IOptions<BackgroundTask> backgroundTask, IServiceScopeFactory serviceScopeFactory)
    {
        this.logger = logger;
        this.bus = bus;
        this.backgroundTask = backgroundTask.Value;
        this.serviceScopeFactory = serviceScopeFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        logger.LogDebug("GracePeriodManagerService is starting.");
        stoppingToken.Register(() => logger.LogDebug("#1 GracePeriodManagerService background task is stopping."));

        while (!stoppingToken.IsCancellationRequested)
        {
            logger.LogDebug("GracePeriodManagerService background task is doing background work.");
            await CheckConfirmedGracePeriodOrdersAsync();
            await Task.Delay(backgroundTask.CheckUpdateTime, stoppingToken);
        }

        logger.LogDebug("GracePeriodManagerService background task is stopping.");
    }

    private async Task CheckConfirmedGracePeriodOrdersAsync()
    {
        logger.LogDebug("Checking confirmed grace period orders");

        var orders = await GetConfirmedGracePeriodOrdersAsync();

        foreach (var order in orders)
        {
            var orderStatusChanged = new OrderStatusChanged(order.Id, order.Status, order.ApplicationUserId);
            logger.LogInformation("----- Publishing event: ({@IntegrationEvent})", orderStatusChanged);
            await bus.Publish(orderStatusChanged);
        }
    }

    private async Task<IEnumerable<Order>> GetConfirmedGracePeriodOrdersAsync()
    {
        IEnumerable<Order> orders = new List<Order>();

        try
        {
            using var scope = serviceScopeFactory.CreateScope();
            using var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            orders = await context.Orders.Include(x => x.ApplicationUser)
                .Where(x => x.Date <= DateTime.UtcNow.AddMinutes(-backgroundTask.GracePeriodTime) && x.Status == OrderStatus.Submited)
                .ToListAsync();
        }
        catch (SqlException exception)
        {
            logger.LogCritical(exception, "FATAL ERROR: {Message}", exception.Message);
        }

        return orders;
    }
}
