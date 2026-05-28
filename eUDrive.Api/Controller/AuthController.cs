using Microsoft.AspNetCore.Mvc;
using eUDrive.BusinessLogic.Interfaces;
using eUDrive.Domains.Models.User;
using Microsoft.AspNetCore.Authorization;

namespace eUDrive.Api.Controller
{
    [AllowAnonymous]
    [Route("/api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserActions _userActions;
        
        public AuthController()
        {
            var bl = new BusinessLogic.BusinessLogic();
            _userActions = bl.GetUserActions();
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] UserAuthDto auth)
        {
            var result = _userActions.LoginAction(auth);

            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }

            return Ok(new
            {
                token = result.Data.Token,
                data = new
                {
                    id = result.Data.Id,
                    username = result.Data.Username,
                    email = result.Data.Email,
                    role = result.Data.Role
                }
            });
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            return Ok(new { IsSuccess = true, message = "Logged Out" });
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] UserRegisterDto user)
        {
            var registerUser = _userActions.CreateUserAction(user);
            
            if (!registerUser.IsSuccess)
            {
                return BadRequest(registerUser);
            }

            return Ok(registerUser);
        }
    }
}
