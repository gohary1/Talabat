using ProductData.Entites;
using ProductRepository.Interfaces;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ProductRepository.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDatabase _database;
        public BasketRepository(IConnectionMultiplexer redis)
        {
            _database=redis.GetDatabase();
        }
        public async Task<bool> DeleteBasketAsync(string basketId)
        {
           return await _database.KeyDeleteAsync(basketId);
        }

        public async Task<CustomerBusket> GetBasketAsync(string id)
        {
            var basket=await _database.StringGetAsync(id);
            return basket.IsNullOrEmpty ? null : JsonSerializer.Deserialize<CustomerBusket>(basket);
        }

        public async Task<CustomerBusket> UpdateBasketAsync(CustomerBusket basket)
        {
            var baskett= await _database.StringSetAsync(basket.Id ,JsonSerializer.Serialize(basket),TimeSpan.FromDays(30));
            return await GetBasketAsync(basket.Id);
        }
    }
}
