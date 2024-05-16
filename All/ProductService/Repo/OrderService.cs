using ProductData.Entites;
using ProductData.Entites.orders;
using ProductRepository;
using ProductRepository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductService.Repo
{
    public class OrderService : IOrderService
    {
        private readonly IBasketRepository _basketRepo;
        private readonly IUnitOfWork _unitOfWork;

        public OrderService(IBasketRepository basketRepo
            ,IUnitOfWork unitOfWork
            )
        {
            _basketRepo = basketRepo;
            _unitOfWork = unitOfWork;
 
        }
        public async Task<Order?> CreateOrderAsync(string BuyerEmail, string basketId, int DelivMethod, Address shippingAddress)
        {
            var basket = await _basketRepo.GetBasketAsync(basketId);

            var orderItems = new List<OrderItem>();

            if (basket.Items.Count() > 0)
            {
                foreach (var item in basket.Items)
                {
                    var product = await _unitOfWork.Repository<Product>().GetByIdAsync(item.Id);
                    var productItem = new OrderItem(product.id, product.Name, product.PictureUrl, product.Price, item.Quantity);
                    orderItems.Add(productItem);
                }
            }
            var subTotal = orderItems.Sum(m => m.Price * m.Quantity);

            var delivMethod = await _unitOfWork.Repository<DeliveryMethod>().GetByIdAsync(DelivMethod);

            var order = new Order(BuyerEmail, shippingAddress, delivMethod, orderItems, subTotal);
            await _unitOfWork.Repository<Order>().AddAsync(order);
            var result=await _unitOfWork.CompleteAsync();
            if (result <= 0) return null;
            return order;
        }


        public async Task<Order> GetOrderByIdAsync(string BuyerEmai, int orderId)
        {
            var specs = new OrderSpec(BuyerEmai,orderId);
            var order = await _unitOfWork.Repository<Order>().GetByIdAsyncWithSpacification(specs);
            return order;
        }

        public async Task<IReadOnlyList<Order>> GetOrderForSpecificUser(string BuyerEmail)
        {
            var specs = new OrderSpec(BuyerEmail);
           var orders = await _unitOfWork.Repository<Order>().GetAllAsyncWithSpacification(specs);
            return orders;
        }
    }
}
