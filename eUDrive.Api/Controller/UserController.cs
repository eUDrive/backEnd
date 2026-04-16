using eUDrive.BusinessLogic.Interfaces;
using eUDrive.Domains.Models.User;
using Microsoft.AspNetCore.Mvc;

namespace eUDrive.Api.Controller
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserActions _userActions;

        public UserController()
        {
            var bl = new BusinessLogic.BusinessLogic();
            _userActions = bl.GetUserActions();
        }

        [HttpGet("all")]
        public IActionResult GetAllUsers()
        {
            var users = _userActions.GetAllUsersAction();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public IActionResult GetUserById(int id)
        {
            var user = _userActions.GetUserByIdAction(id);

            if (user == null)
            {
                return NotFound(new { message = "User not found" });
            }

            return Ok(user);
        }

        [HttpPost]
        public IActionResult CreateUser([FromBody] UserDto user)
        {
            var createdUser = _userActions.CreateUserAction(user);

            if (!createdUser.IsSuccess)
            {
                return BadRequest(createdUser);
            }

            return Ok(createdUser);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, [FromBody] UserDto user)
        {
            user.Id = id;
            var updatedUser = _userActions.UpdateUserAction(user);
            
            if (!updatedUser.IsSuccess)
            {
                return BadRequest(updatedUser);
            }

            return Ok(updatedUser);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            var user = _userActions.DeleteUserAction(id);

            if (!user.IsSuccess)
            {
                return BadRequest(user);
            }

            return Ok(user);
        }
    }
}
