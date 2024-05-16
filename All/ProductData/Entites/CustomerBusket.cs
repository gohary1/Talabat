using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductData.Entites
{
    public class CustomerBusket
    {
        public string Id { get; set; }
        public List<BasketItem> Items { get; set; }
    }
}
