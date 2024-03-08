namespace SagaGuild.Common.Contracts;


public class OrderSubmittedMessage
{
    public Guid ShopId { get; init; }
}

public class BakeBunRequestedMessage
{
    public Guid ShopId { get; init; }
}

public class BunBakedMessage
{
    public Guid ShopId { get; init; }
}

public class FryPattyRequestedMessage
{
    public Guid ShopId { get; init; }
}

public class PattyFriedMessage
{
    public Guid ShopId { get; init; }
}

public class AssembleBurgerRequestedMessage
{
    public Guid ShopId { get; init; }
}

public class BurgerAssembledMessage
{
    public Guid ShopId { get; init; }
}

