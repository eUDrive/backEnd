using Microsoft.AspNetCore.Mvc;
using eUDrive.BusinessLogic.Interfaces;
using eUDrive.Domains.Models.User;

namespace eUDrive.Api.Controller
{
    [Route("/api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IUserActions _userActions;
        
        public AuthController()
        {
            var bl = new BusinessLogic.BusinessLogic();
            _userActions = bl.GetUserActions();
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] UserAuthDto auth)
        {
            var user = _userActions.LoginAction(auth);

            if (!user.IsSuccess)
            {
                return BadRequest(user);
            }

            return Ok(user);
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] UserDto user)
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
