using Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebMain.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductServiceRpc.ProductServiceRpcClient _client;

        public ProductController(ProductServiceRpc.ProductServiceRpcClient client)
        {
            _client = client;
        }

        // Получить все продукты
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _client.GetAllAsync(new Empty());
            return Ok(response.Products);
        }

        // Добавить новый продукт
        [HttpPost]
        public async Task<IActionResult> AddProduct([FromBody] AddProductRequest request)
        {
            var response = await _client.AddProductAsync(request);

            if (!response.Success)
                return BadRequest(response.Message);

            return Ok(new { message = response.Message });
        }
    }
}
