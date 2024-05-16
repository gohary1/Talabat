using ProductData.Entites.orders;
using System.ComponentModel.DataAnnotations;

namespace ProductApi.DTOs
{
    public class OrderDto
    {
        public string BasketId { get; set; }
        public int DelivaryMethod { get; set; }
        public Address Address { get; set; }
    }
}
