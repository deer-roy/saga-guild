using MassTransit;
using Microsoft.Extensions.Logging;
using SagaGuild.Common.Contracts;

namespace SagaGuild.Services.Consumers;

public class BakeBunConsumer: IConsumer<BakeBunRequestedMessage>
{
    private readonly ILogger<MyFirstConsumer> _logger;
    
    public BakeBunConsumer(
        ILogger<MyFirstConsumer> logger
    )
    {
        _logger = logger;
    }
    
    public async Task Consume
    (
        ConsumeContext<BakeBunRequestedMessage> context
    )
    {
        await context.Publish(new BunBakedMessage(
            context.Message.ShopId
            )
        );
    }
}
