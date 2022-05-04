using MassTransit;
using Shared.Messages.Commands;
using Shared.Messages.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockService.Consumer
{
    public class OrderValidateConsumer : IConsumer<IOrderValidate>
    {
        public async Task Consume(ConsumeContext<IOrderValidate> context)
        {
            var data = context.Message;

            //TO DO:
            if (data.Price == 0)
            {
                await context.Publish<IOrderCancelled>(new
                {
                    OrderId = context.Message.OrderId,
                    Price = context.Message.Price
                });
            }
            else
            {
                // send to next microservice
            }
        }
    }
}
