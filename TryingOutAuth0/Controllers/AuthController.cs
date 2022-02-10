using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System.Threading.Tasks;
using TryingOutAuth0.Auth.Auth0Client;
using TryingOutAuth0.ViewModels;

namespace TryingOutAuth0.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthClientService _authClientService;

        public AuthController(AuthClientService authClientService)
        {
            _authClientService = authClientService;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginRequestViewModel request)
        {
            var test = await _authClientService.LoginAsync(request.Email, request.Password);
            return Ok(test);
        }

        [HttpGet]
        public async Task<IActionResult> Email()
        {
            var accessToken = Request.Headers[HeaderNames.Authorization].ToString().Split(" ")[1] ?? "";
            var test = await _authClientService.GetEmailAsync(accessToken);
            return Ok(test);
        }
    }
}
