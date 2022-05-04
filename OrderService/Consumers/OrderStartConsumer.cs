using MassTransit;
using Shared.Messages.Commands;
using Shared.Messages.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderService.Consumers
{
    public class OrderStartConsumer : IConsumer<IOrderInitiate>
    {
        public async Task Consume(ConsumeContext<IOrderInitiate> context)
        {
            await context.Publish<IOrderStarted>(new
            {
                context.Message.OrderId,
                context.Message.Price,
                context.Message.Product
            });
        }
    }
}
