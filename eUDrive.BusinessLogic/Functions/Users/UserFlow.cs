using eUDrive.BusinessLogic.Core.User;
using eUDrive.BusinessLogic.Interfaces;
using eUDrive.Domains.Models.Base;
using eUDrive.Domains.Models.User;

namespace eUDrive.BusinessLogic.Functions.Users
{
    public class UserFlow : UserAction, IUserActions
    {
        public List<UserDto> GetAllUsersAction()
        {
            return ExecuteGetAllUsersAction();
        }

        public UserDto? GetUserByIdAction(int id)
        {
            return ExecuteGetUserByIdAction(id);
        }

        public ResponseMsg CreateUserAction(UserRegisterDto user)
        {
            return ExecuteCreateUserAction(user);
        }

        public ResponseMsg UpdateUserAction(UserUpdateDto user)
        {
            return ExecuteUpdateUserAction(user);
        }

        public ResponseMsg ActivateUserAction(UserActivateDto user)
        {
            return ExecuteActivateUserAction(user);
        }

        public ResponseMsg DeleteUserAction(int id)
        {
            return ExecuteDeleteUserAction(id);
        }

        public ResponseAction<LoginResponseDto> LoginAction(UserAuthDto user)
        {
            return ExecuteLoginAction(user);
        }
    }
}
