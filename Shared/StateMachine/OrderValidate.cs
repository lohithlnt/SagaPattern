using Shared.DbCOnfiguration;
using Shared.Messages.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.StateMachine
{
    public class OrderValidate: IOrderValidate
    {
        private readonly OrderState orderSaga;
        public OrderValidate(OrderState orderState)
        {
            this.orderSaga = orderState;
        }
        public Guid OrderId => orderSaga.OrderId;
        public decimal Price => orderSaga.Price;
        public string Product => orderSaga.Product;
    }
}
