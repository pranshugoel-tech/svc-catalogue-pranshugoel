using Microsoft.AspNetCore.Mvc;
using Service.Catalogue.Api.Services;

namespace Service.Catalogue.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly JwtTokenService _jwtTokenService;

        public AuthController(JwtTokenService jwtTokenService)
        {
            _jwtTokenService = jwtTokenService;
        }

        // Example: POST /api/auth/login
        [HttpPost("login")]
        public IActionResult Login(string username = "admin", string password = "password")
        {
            // ⚠️ In real life, validate user against DB/IdP
            if (username == "admin" && password == "password")
            {
                var token = _jwtTokenService.GenerateToken(username);
                return Ok(new { access_token = token });
            }

            return Unauthorized();
        }
    }
}
