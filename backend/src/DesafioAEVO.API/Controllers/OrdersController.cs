using DesafioAEVO.Application.UseCases.Order;
using DesafioAEVO.Communication.Requests;
using DesafioAEVO.Communication.Responses;
using Microsoft.AspNetCore.Mvc;

namespace DesafioAEVO.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(typeof(OrderResponse), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<OrderResponse>>> GetAllAsync([FromServices] IGetAllOrdersUseCase useCase)
        {
            var orders = await useCase.ExecuteAsync();
            return Ok(orders);
        }

        [HttpPost]
        [ProducesResponseType(typeof(CreateOrderResponse), StatusCodes.Status201Created)]
        public async Task<ActionResult> CreateOrderAsync([FromServices] ICreateOrderUseCase useCase, [FromBody] RequestOrderJson request)
        {
            var response = await useCase.ExecuteAsync(request);
            return Created(string.Empty, response);
        }
    }
}
