using MassTransit;

namespace GimenaCreations.Consumers;

public class OrderStatusChangedConsumerConfiguration : ConsumerDefinition<OrderStatusChangedConsumer>
{
    protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator, IConsumerConfigurator<OrderStatusChangedConsumer> consumerConfigurator)
    {
        endpointConfigurator.UseMessageRetry(r => r.Intervals(500, 1000));
        base.ConfigureConsumer(endpointConfigurator, consumerConfigurator);
    }
}
