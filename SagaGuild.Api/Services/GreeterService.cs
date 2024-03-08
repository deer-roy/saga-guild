using Grpc.Core;
using MassTransit;
using SagaGuild.Api;
using SagaGuild.Common.Contracts;

namespace SagaGuild.Api.Services;

public class GreeterService : Greeter.GreeterBase
{
    private readonly ILogger<GreeterService> _logger;
    private readonly IPublishEndpoint _publishEndpoint;
    public GreeterService(ILogger<GreeterService> logger,
        IPublishEndpoint publishEndpoint
    )
    {
        _logger = logger;
        _publishEndpoint = publishEndpoint;
    }

    public override async Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
    {
        _logger.LogInformation("Publishing message");
        await _publishEndpoint.Publish(new MyFirstMessage());
        await _publishEndpoint.Publish(new OrderSubmittedMessage(Guid.NewGuid()));
        return new HelloReply
        {
            Message = "Hello " + request.Name
        };
    }
}
