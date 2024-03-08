using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using SagaGuild.Services.Consumers;

var builder = Host.CreateApplicationBuilder();
builder.Configuration.AddJsonFile("appsettings.json");

builder.Services.AddMassTransit(config => {
    // mass transit config  
    config.AddConsumer<MyFirstConsumer>();
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
            
            cfg.ConfigureEndpoints(context);
        });
});


builder.Build().Run();