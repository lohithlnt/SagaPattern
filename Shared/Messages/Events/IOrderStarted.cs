using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Messages.Events
{
    public interface IOrderStarted
    {
        public Guid OrderId { get; set; }
        public decimal Price { get; }
        public string Product { get; }
    }
}
