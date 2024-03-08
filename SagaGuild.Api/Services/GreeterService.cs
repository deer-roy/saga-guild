using Grpc.Core;
using MassTransit;
using SagaGuild.Api;
using SagaGuild.Common.Contracts;

namespace SagaGuild.Api.Services;

public class GreeterService : Greeter.GreeterBase
{
    private readonly ILogger<GreeterService> _logger;
    private readonly IPublishEndpoint _publishEndpoint;
    
    private readonly Guid _shop1 = Guid.Parse("ed7390be-5c16-45d3-91dc-003f97bf2c35");
    private readonly Guid _shop2 = Guid.Parse("ac35a904-40bc-42e3-8056-457ef92df202");
    private readonly Guid _shop3 = Guid.Parse("a858ab24-14e7-42fb-b95b-f97afbc8fe0a");
    
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
        await _publishEndpoint.Publish(new OrderSubmittedMessage
        {
            ShopId = _shop1
        });
        return new HelloReply
        {
            Message = "Hello " + request.Name
        };
    }
}
