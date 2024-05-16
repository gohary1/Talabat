using ProductData.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductRepository.Interfaces
{
    public interface IBasketRepository
    {
        public Task<CustomerBusket> GetBasketAsync(string basketId);
        public Task<bool> DeleteBasketAsync(string basketId);
        public Task<CustomerBusket> UpdateBasketAsync(CustomerBusket basket);
    }
}
