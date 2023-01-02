using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Basket.API
{
    public class BasketConfig
    {
        public string ConnectionString { get; set; }
        public string EventBusConnection { get; set; }
    }
}
