using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.BusConfiguration
{
    public class BusConstants
    {
        public const string RabbitMqUri = "rabbitmq://localhost/";
        public const string Username = "guest";
        public const string Password = "guest";
        public const string OrderQueue = "order-start"; //based upon consumer class: OrderStart
    }
}
