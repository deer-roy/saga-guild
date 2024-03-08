using MassTransit;
using Microsoft.Extensions.Logging;
using SagaGuild.Common.Contracts;

namespace SagaGuild.Services.Consumers;

public class MyFirstConsumer: IConsumer<MyFirstMessage>
{
    private readonly ILogger<MyFirstConsumer> _logger;
    
    public MyFirstConsumer(
        ILogger<MyFirstConsumer> logger
    )
    {
        _logger = logger;
    }
    
    public async Task Consume
    (
        ConsumeContext<MyFirstMessage> context
    )
    {
        _logger.LogInformation("The message has been consumed, sir.");      
    }
}