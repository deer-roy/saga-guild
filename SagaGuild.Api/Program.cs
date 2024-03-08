using MassTransit;
using SagaGuild.Api.Services;

var builder = WebApplication.CreateBuilder(args);

// Additional configuration is required to successfully run gRPC on macOS.
// For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682

// Add services to the container.
builder.Services.AddGrpc();
builder.Services.AddGrpcReflection();

builder.Services.AddMassTransit(config => {
    // mass transit config  
    config.UsingRabbitMq(
        (
            context,
            cfg
        ) => {
            cfg.Host(
                builder.Configuration["RabbitMQ:Host"],
                builder.Configuration["RabbitMQ:VirtualHost"],
                c => {
                    c.Username(builder.Configuration["RabbitMQ:User"]);
                    c.Password(builder.Configuration["RabbitMQ:Password"]);
                }
            );
        });
});



var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcReflectionService();
app.MapGrpcService<GreeterService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
