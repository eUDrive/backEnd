using Microsoft.AspNetCore.Mvc;
using eUDrive.BusinessLogic.Interfaces;
using eUDrive.Domains.Models.User;
using eUDrive.DataAccess.Context;

namespace eUDrive.Api.Controller
{
    [Route("/api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserActions _userActions;
        private readonly ISessionActions _sessionActions;
        
        public AuthController()
        {
            var bl = new BusinessLogic.BusinessLogic();
            _userActions = bl.GetUserActions();
            _sessionActions = bl.GetSessionActions();
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] UserAuthDto auth)
        {
            var result = _userActions.LoginAction(auth);

            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }

            using (var db = new UserContext())
            {
                var email = auth.Email.ToLower();
                var user = db.Users.FirstOrDefault(u => u.IsActive && u.Email.ToLower() == email);

                if (user == null)
                {
                    return BadRequest(new { IsSuccess = false, message = "User not found after login" });
                }

                var sessionKey = _sessionActions.CreateOrUpdateSession(user.Id);

                Response.Cookies.Append("X-KEY", sessionKey, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict,
                    Expires = DateTimeOffset.Now.AddMinutes(60)
                });
            }

            return Ok(result);
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            var cookie = Request.Cookies["X-KEY"];

            if (!string.IsNullOrWhiteSpace(cookie))
            {
                _sessionActions.DeleteSession(cookie);
            }

            Response.Cookies.Append("X-KEY", "", new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTimeOffset.Now.AddDays(-1)
            });

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
