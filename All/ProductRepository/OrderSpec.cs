using ProductData.Entites.orders;
using ProductData.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductRepository
{
    public class OrderSpec:BaseSpacificaton<Order,int>
    {
        public OrderSpec(string email)
            :base(e=>
                 (string.IsNullOrEmpty(email)||e.BuyerEmail==email)
                 )
        {
            Includes.Add(e => e.DeliveryMethod);
            Includes.Add(e => e.OrderItems);
            AddOrderByDesc(p => p.OrderDate);
        }
        public OrderSpec(string email,int id)
            :base(e=>
                 (string.IsNullOrEmpty(email)||e.BuyerEmail==email)&&
                (id==null)||(e.id==id)
                 )
        {
            Includes.Add(e => e.DeliveryMethod);
            Includes.Add(e => e.OrderItems);
        }
    }
}
