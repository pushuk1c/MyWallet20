using Microsoft.AspNetCore.Mvc;
using MyWallet.DTOs.User;
using MyWallet.Models;
using System.Runtime.CompilerServices;

namespace MyWallet.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        public IAuthRepository _authRepository;

        public AuthController(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        [HttpPost("Register")]
        public async Task<ActionResult<ServiceResponse<Guid>>> Register(UserRegisterDto request)
        {
            var response = await _authRepository.Register(
                new User { name = request.username}, request.password);
           
            if(!response.success)
                return BadRequest(response);

            return Ok(response);
        }

        [HttpPost("Login")]
        public async Task<ActionResult<ServiceResponse<Guid>>> Login(UserLoginDto request)
        {
            var response = await _authRepository.Login(request.username, request.password);

            if (!response.success)
                return BadRequest(response);

            return Ok(response);
        }


    }
}
