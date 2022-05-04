using Automatonymous;
using Shared.DbCOnfiguration;
using Shared.Messages.Events;
using System;

namespace Shared.StateMachine
{
    public class OrderStateMachine : MassTransitStateMachine<OrderState>
    {
        public OrderStateMachine()
        {
            Event(() => OrderStated, x => x.CorrelateById(m => m.Message.OrderId));
            Event(() => OrderCancelled, x => x.CorrelateById(m => m.Message.OrderId));

            InstanceState(x => x.CurrentState);

            Initially(
                   When(OrderStated)
                   .Then(ctx =>
                   {
                       ctx.Instance.OrderCreationDateTime = DateTime.Now;
                       ctx.Instance.OrderId = ctx.Data.OrderId;
                       ctx.Instance.Product = ctx.Data.Product;
                       ctx.Instance.Price = ctx.Data.Price;
                   })
                   .TransitionTo(Started)
                   .Publish(ctx => new OrderValidate(ctx.Instance)));

            During(Started, When(OrderCancelled)
                   .Then(ctx => ctx.Instance.OrderCancelDateTime = DateTime.Now)
                   .TransitionTo(Cancelled));
        }

        public State Started { get; set; }
        public State Cancelled { get; set; }
        public Event<IOrderStarted> OrderStated { get; set; }
        public Event<IOrderCancelled> OrderCancelled { get; set; }
    }
}
