using MassTransit;
using MassTransit.Saga;
using SagaGuild.Common.Contracts;

namespace SagaGuild.Services.Sagas;

public class BurgerSagaInstance: SagaStateMachineInstance
{
    public Guid CorrelationId { get; set; }
    public string CurrentState { get; set; } 
}


public class BurgerSagaStateMachine : MassTransitStateMachine<BurgerSagaInstance>
{
    #region states
   public State BakingBun { get; set; } 
   public State FryingPatty { get; set; }
   public State AssemblingBurger { get; set; }
   #endregion

   #region events
   public Event<OrderSubmittedMessage> OrderSubmittedEvent { get; set; }
   public Event<BunBakedMessage> BunBakedEvent { get; set; }
   public Event<PattyFriedMessage> PattyFriedEvent { get; set; }
   public Event<BurgerAssembledMessage> BurgerAssembledEvent { get; set; }
   #endregion
   
   public BurgerSagaStateMachine()
   {
       InstanceState(x => x.CurrentState);
       
       #region correlate events

       Event(
           () => OrderSubmittedEvent, 
           e => e.CorrelateById(x => x.Message.ShopId)
           );
       Event(
           () => BunBakedEvent, 
           e => e.CorrelateById(x => x.Message.ShopId)
           );
       Event(
           () => PattyFriedEvent, 
           e => e.CorrelateById(x => x.Message.ShopId)
           );
       Event(
           () => BurgerAssembledEvent, 
           e => e.CorrelateById(x => x.Message.ShopId)
           );

       #endregion

       #region flow
       Initially(
           When(OrderSubmittedEvent)
               .TransitionTo(BakingBun)
               .PublishAsync(context => context.Init<BakeBunRequestedMessage>(
                   new BakeBunRequestedMessage(context.Saga.CorrelationId)
               ))
           );

       During(
           BakingBun,
           When(BunBakedEvent) 
               .TransitionTo(FryingPatty)
               .PublishAsync(context => context.Init<FryPattyRequestedMessage>(
                   new FryPattyRequestedMessage(context.Saga.CorrelationId)
               ))
       );
       
       During(
           FryingPatty,
           When(PattyFriedEvent) 
               .TransitionTo(AssemblingBurger)
               .PublishAsync(context => context.Init<AssembleBurgerRequestedMessage>(
                   new AssembleBurgerRequestedMessage(context.Saga.CorrelationId)
               ))
       );
       
       During(
           AssemblingBurger,
           When(BurgerAssembledEvent) 
               .Finalize()
       );
       
       DuringAny(
           Ignore(OrderSubmittedEvent)
       );
       
       #endregion
   }
}