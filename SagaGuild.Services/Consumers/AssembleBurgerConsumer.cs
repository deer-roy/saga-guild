using MassTransit;
using Microsoft.Extensions.Logging;
using SagaGuild.Common.Contracts;

namespace SagaGuild.Services.Consumers;

public class AssembleBurgerConsumer : IConsumer<AssembleBurgerRequestedMessage>
{
    private readonly ILogger<MyFirstConsumer> _logger;

    public AssembleBurgerConsumer
    (
        ILogger<MyFirstConsumer> logger
    )
    {
        _logger = logger;
    }

    public async Task Consume
    (
        ConsumeContext<AssembleBurgerRequestedMessage> context
    )
    {
        await Task.Delay(1000);
        _logger.LogInformation("Burger assembled");
        await context.Publish(new BurgerAssembledMessage
            {
                ShopId = context.Message.ShopId
            }
        );
    }
}