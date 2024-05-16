using ProductData.Entites.orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductRepository.Interfaces
{
    public interface IOrderService
    {
        Task<Order> CreateOrderAsync(string BuyerEmail, string basketId, int DelivMethod, Address shippingAddress);
        Task<IReadOnlyList<Order>> GetOrderForSpecificUser(string BuyerEmai);
         Task<Order> GetOrderByIdAsync(string BuyerEmai, int orderId);
    }
}
