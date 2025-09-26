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
        public IActionResult Login([FromBody] LoginRequest request)
        {
            // ⚠️ In real life, validate user against DB/IdP
            if (request.Username == "admin" && request.Password == "password")
            {
                var token = _jwtTokenService.GenerateToken(request.Username);
                return Ok(new { access_token = token });
            }

            return Unauthorized();
        }
    }

    public class LoginRequest
    {
        public string Username { get; set; } = "";
        public string Password { get; set; } = "";
    }
}
