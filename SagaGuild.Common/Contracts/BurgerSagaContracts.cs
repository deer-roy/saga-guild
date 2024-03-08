namespace SagaGuild.Common.Contracts;


public record OrderSubmittedMessage(
    Guid ShopId
);

public record BakeBunRequestedMessage(
    Guid ShopId
);

public record BunBakedMessage(
    Guid ShopId
);

public record FryPattyRequestedMessage(
    Guid ShopId
);

public record PattyFriedMessage(
    Guid ShopId
);

public record AssembleBurgerRequestedMessage(
    Guid ShopId
);

public record BurgerAssembledMessage(
    Guid ShopId
);

