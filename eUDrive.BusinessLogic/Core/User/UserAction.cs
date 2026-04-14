using eUDrive.DataAccess.Context;
using eUDrive.Domains.Entities.User;
using eUDrive.Domains.Models.Base;
using eUDrive.Domains.Models.User;
using System.Security.Cryptography;
using System.Text;

namespace eUDrive.BusinessLogic.Core.User
{
    public class UserAction
    {
        private string HashPassword(string password)
        {
            using (var md5 = MD5.Create())
            {
                var originalBytes = Encoding.Default.GetBytes(password + "VeryHardToGuessString");
                var encodeBytes = md5.ComputeHash(originalBytes);

                return BitConverter.ToString(encodeBytes).Replace("-", "").ToLower();
            }


        }

        private bool VerifyPassword(string password, string hash)
        {
            var hashOutput = HashPassword(password);
            return hashOutput == hash;
        }

        protected List<UserDto> ExecuteGetAllUsersAction()
        {
            using (var db = new UserContext())
            {
                    return db.Users
                    .Where(u => u.IsActive)
                    .Select(u => new UserDto
                    {
                        Id = u.Id,
                        Username = u.Username,
                        Email = u.Email,
                    })
                    .ToList();
            }
        }

        protected UserDto ExecuteGetUserByIdAction(int id) 
        {
            using (var db = new UserContext())
            {
                var user = db.Users.FirstOrDefault(u => u.Id == id && u.IsActive);

                if (user == null)
                {
                    return null;
                }

                return new UserDto
                {
                    Id = user.Id,
                    Username = user.Username,
                    Email = user.Email,
                };
            }
        }

        protected ResponseMsg ExecuteCreateUserAction(UserDto userDto)
        {
            //Name should not be empty so we check it
            if (string.IsNullOrEmpty(userDto.Username))
            {
                return new ResponseMsg
                {
                    IsSuccess = false,
                    Message = "Имя не должнл быть пустым. "
                };
            }
        }
    }
}
