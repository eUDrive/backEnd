using eUDrive.Domains.Models.User;
using eUDrive.Domains.Models.Base;

namespace eUDrive.BusinessLogic.Interfaces
{
    public interface IUserActions
    {
        List<UserDto> GetAllUsersAction();
        UserDto? GetUserByIdAction(int id);

        ResponseMsg CreateUserAction(UserRegisterDto user);
        ResponseMsg UpdateUserAction(UserUpdateDto user);
        ResponseMsg ActivateUserAction(UserActivateDto user);
        ResponseMsg DeleteUserAction(int id);
        ResponseAction<LoginResponseDto> LoginAction(UserAuthDto userAuth);
    }
}
