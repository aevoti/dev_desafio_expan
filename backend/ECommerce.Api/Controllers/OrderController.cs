using Microsoft.AspNetCore.Mvc;
using OrderApi.Models;
using OrderApi.Services;
using System.Threading.Tasks;

namespace OrderApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderRequest request)
        {
            if (request == null || request.Items == null || request.Items.Count == 0)
                return BadRequest("Pedido inválido.");

            var order = await _orderService.CreateOrderAsync(request);
            return Ok(order);
        }
    }
}
