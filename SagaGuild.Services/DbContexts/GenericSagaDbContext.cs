using MassTransit;
using MassTransit.EntityFrameworkCoreIntegration;
using Microsoft.EntityFrameworkCore;
using SagaGuild.Services.Sagas;

namespace SagaGuild.Common.DbContexts;

public class GenericSagaDbContext : SagaDbContext
{

    public GenericSagaDbContext
    (
        DbContextOptions options
    ) : base(options)
    {
    }

    protected override IEnumerable<ISagaClassMap> Configurations
    {
        get
        {
            yield return new BurgerSagaStateMap();
        }
    }
}

public class BurgerSagaStateMap : SagaClassMap<BurgerSagaInstance>
{
}