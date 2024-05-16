using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using ProductApi.Error;
using ProductData.Entites;
using ProductRepository.Interfaces;

namespace ProductApi.Controllers
{

    public class BasketController : BaseController
    {
        private readonly IBasketRepository _basketRepository;

        public BasketController(IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
        }

        [HttpGet("GetBasket/{id}")]
        public async Task<ActionResult<CustomerBusket>> GetBasket(string id)
        {
            var basket = await _basketRepository.GetBasketAsync(id);
            return Ok(basket);
        }
        [HttpPost("updateBasket")]
        public async Task<ActionResult<CustomerBusket>> UpdateBasket(CustomerBusket baskett)
        {
            var basket = await _basketRepository.UpdateBasketAsync(baskett);
            if (basket == null) { return BadRequest(new ApiResponse(400)); }
            return Ok(basket);
        }
        [HttpGet("DeleteBasket/{id}")]
        public async Task<ActionResult<CustomerBusket>> DeleteBasket(string id)
        {
            var basket = await _basketRepository.DeleteBasketAsync(id);
            return Ok(basket);
        }
    }
}
