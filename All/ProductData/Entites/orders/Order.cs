using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductData.Entites.orders
{
    public class Order:BaseEntity<int>
    {
        public Order()
        {
            
        }

        public Order(string buyerEmail, Address shippingAddress, DeliveryMethod deliveryMethod, ICollection<OrderItem> orderItems, decimal subTotal)
        {
            BuyerEmail = buyerEmail;
            this.status = status;
            this.shippingAddress = shippingAddress;
            DeliveryMethod = deliveryMethod;
            OrderItems = orderItems;
            SubTotal = subTotal;
        }

        public string BuyerEmail { get; set; }
        public DateTimeOffset OrderDate { get; set; }=DateTimeOffset.UtcNow;
        public OrderStatus status { get; set; } = OrderStatus.Pending;
        public Address shippingAddress { get; set; }
        [ForeignKey("DeliveryMethod")]
        public int DeliveryMethodId { get; set; }
        public DeliveryMethod DeliveryMethod { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; } = new HashSet<OrderItem>();

        public decimal SubTotal { get; set; }

        public decimal TotalTotal()
            => SubTotal + DeliveryMethod.Cost;

        public string PaymentId { get; set; } = string.Empty;
    }
}
