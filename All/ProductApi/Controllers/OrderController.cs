using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductApi.DTOs;
using ProductApi.Error;
using ProductData.Entites.orders;
using ProductRepository.Interfaces;
using System.Security.Claims;

namespace ProductApi.Controllers
{

    public class OrderController : BaseController
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public OrderController(IOrderService orderService,IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("createOrder")]
        public async Task<ActionResult<Order>> CreateOrdere(OrderDto order)
        {
            var buyerEmail=User.FindFirstValue(ClaimTypes.Email);
            var Order = await _orderService.CreateOrderAsync(buyerEmail, order.BasketId, order.DelivaryMethod, order.Address);
            if (Order == null) return BadRequest(new ApiResponse(400));
            return Ok(Order);
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("GetOrderForUser")]
        public async Task<ActionResult<Order>> GetOrderForUser()
        {
            var buyerEmail=User.FindFirstValue(ClaimTypes.Email);
            var Order = await _orderService.GetOrderForSpecificUser(buyerEmail);
            if (Order == null) return BadRequest(new ApiResponse(400));
            return Ok(Order);
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("GetOrderForSpecUser")]
        public async Task<ActionResult<Order>> GetOrderForUser(int id)
        {
            var buyerEmail=User.FindFirstValue(ClaimTypes.Email);
            var Order = await _orderService.GetOrderByIdAsync(buyerEmail,id);
            if (Order == null) return BadRequest(new ApiResponse(400));
            return Ok(Order);
        }
    }
}
