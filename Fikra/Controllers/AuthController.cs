using Fikra.UserManagement.Modules.Auth;
using Fikra.UserManagement.Modules.Auth.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Fikra.Controllers
{
    [Route("auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        #region Constructors
        private readonly IAuthRepository _authRepository;
        public AuthController(IAuthRepository authRepository)
        {
            this._authRepository = authRepository;
        }
        #endregion

        #region Actions
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterUserDTO registerUserDTO)
        {
            await _authRepository.RegisterUser(registerUserDTO);
            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginCredentials credentials)
        {
            var result = await _authRepository.LoginUser(credentials);
            return Ok(result);
        }
        #endregion
    }
}
