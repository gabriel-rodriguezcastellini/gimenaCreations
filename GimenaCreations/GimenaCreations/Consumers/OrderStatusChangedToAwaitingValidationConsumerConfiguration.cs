using MassTransit;

namespace GimenaCreations.Consumers;

public class OrderStatusChangedToAwaitingValidationConsumerConfiguration : ConsumerDefinition<OrderStatusChangedToAwaitingValidationConsumer>
{
    protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator, IConsumerConfigurator<OrderStatusChangedToAwaitingValidationConsumer> consumerConfigurator)
    {
        endpointConfigurator.UseMessageRetry(r => r.Intervals(500, 1000));
        base.ConfigureConsumer(endpointConfigurator, consumerConfigurator);
    }
}
