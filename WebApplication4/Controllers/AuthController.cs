using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication4.Interface;
using WebApplication4.Models;

namespace WebApplication4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService authService;

        public AuthController(IAuthService authService)
        {
            this.authService = authService;
        }

        [HttpPost("Register") ]
        public async Task<IActionResult> SaveUser([FromBody] RegisterModel model)
        {
            if(!ModelState.IsValid)
            return BadRequest(ModelState);

            var res = await authService.RegisterAsync(model);
            
            if(!res.IsAuth)
                return BadRequest(res.Message);
            return Ok(res);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> LoginUser([FromBody] LoginModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var res = await authService.LoginAsync(model);

            if (!res.IsAuth)
                return BadRequest(res.Message);
            return Ok(res);
        }

    }
}
