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
   public State Packaging { get; set; }
   #endregion

   #region events
   public Event<OrderSubmittedMessage> OrderSubmittedEvent { get; set; }
   #endregion
   
   public BurgerSagaStateMachine()
   {
       InstanceState(x => x.CurrentState);
       
       #region correlate events

       Event(
           () => OrderSubmittedEvent, 
           e => e.CorrelateById(x => x.Message.ShopId)
           );

       #endregion

   }
}