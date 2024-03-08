using MassTransit;
using Microsoft.Extensions.Logging;
using SagaGuild.Common.Contracts;

namespace SagaGuild.Services.Consumers;

public class FryPattyConsumer : IConsumer<FryPattyRequestedMessage>
{
    private readonly ILogger<MyFirstConsumer> _logger;

    public FryPattyConsumer
    (
        ILogger<MyFirstConsumer> logger
    )
    {
        _logger = logger;
    }

    public async Task Consume
    (
        ConsumeContext<FryPattyRequestedMessage> context
    )
    {

        _logger.LogInformation("Patty fried");
        await context.Publish(
            new PattyFriedMessage
            {
                ShopId = context.Message.ShopId
            }
        );
    }
}