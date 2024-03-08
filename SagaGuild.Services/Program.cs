using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SagaGuild.Common.DbContexts;
using SagaGuild.Services.Consumers;
using SagaGuild.Services.Sagas;

var builder = Host.CreateApplicationBuilder();
builder.Configuration.AddJsonFile("appsettings.json");

builder.Services.AddMassTransit(config => {
    // mass transit config  
    config.AddDbContext<GenericSagaDbContext>(opts => {
        opts.UseNpgsql(builder.Configuration["DbConnection"]);
    });
    config.AddConsumer<MyFirstConsumer>();
    config.AddSagaStateMachine<BurgerSagaStateMachine, BurgerSagaInstance>();
    config.AddSagaRepository<BurgerSagaInstance>()
        .EntityFrameworkRepository(rabbitCfg => {
            rabbitCfg.UsePostgres();
            rabbitCfg.ExistingDbContext<GenericSagaDbContext>();
        });
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