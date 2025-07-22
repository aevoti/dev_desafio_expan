using DesafioAEVO.Application.UseCases.Product;
using DesafioAEVO.Communication.Requests;
using DesafioAEVO.Communication.Responses;
using Microsoft.AspNetCore.Mvc;

namespace DesafioAEVO.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {

        [HttpGet]
        [ProducesResponseType(typeof(ProductResponse), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ProductResponse>>> GetAllAsync([FromServices] IGetAllProductsUseCase useCase)
        {
            var products = await useCase.ExecuteAsync();
            return Ok(products);
        }

        [HttpPost()]
        [ProducesResponseType(typeof(CreateProductResponse), StatusCodes.Status201Created)]
        public async Task<ActionResult> CreateProductAsync([FromServices] ICreateProductUseCase useCase, [FromBody] RequestProductJson request)
        {
            var response = await useCase.ExecuteAsync(request);
            return Created(string.Empty, response);
        }
    }
}
