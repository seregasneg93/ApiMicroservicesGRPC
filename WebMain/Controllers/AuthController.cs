using Contracts;
using Grpc.Net.Client;
using Microsoft.AspNetCore.Mvc;

namespace WebMain.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserServiceRpc.UserServiceRpcClient _client;

        public AuthController(UserServiceRpc.UserServiceRpcClient client)
        {
            _client = client;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            var response = await _client.RegisterAsync(request);
            return Ok(response);
        }
    }
}
