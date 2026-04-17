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

        protected UserDto? ExecuteGetUserByIdAction(int id) 
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

        protected ResponseMsg ExecuteCreateUserAction(UserRegisterDto userDto)
        {
            if (string.IsNullOrWhiteSpace(userDto.Username))
            {
                return new ResponseMsg
                {
                    IsSuccess = false,
                    Message = "Username shouldn't be empty. "
                };
            }

            if (string.IsNullOrWhiteSpace(userDto.Email) || !userDto.Email.Contains("@"))
            {
                return new ResponseMsg
                {
                    IsSuccess = false,
                    Message = "Invalid Email format"
                };
            }

            if (string.IsNullOrWhiteSpace(userDto.Password))
            {
                return new ResponseMsg
                {
                    IsSuccess = false,
                    Message = "Password should be at least 8 characters"
                };
            }

            var email = userDto.Email.ToLower();
            var username = userDto.Username.ToLower();

            using (var db = new UserContext())
            {
                var existingUserByEmail = db.Users.FirstOrDefault(u => u.Email.ToLower() == email);

                if (existingUserByEmail != null)
                {
                    return new ResponseMsg
                    {
                        IsSuccess = false,
                        Message = "User with this email already exists"
                    };
                }

                var existingUserByUsername = db.Users.FirstOrDefault(u => u.Username.ToLower() == username);

                if (existingUserByUsername != null) 
                {
                    return new ResponseMsg
                    {
                        IsSuccess = false,
                        Message = "This Username already exists"
                    };
                        
                };
            }

            var userData = new UserData()
            {
                Username = userDto.Username,
                Email = userDto.Email,
                PasswordHash = HashPassword(userDto.Password),
                CreatedAt = DateTime.Now,
                IsActive = true
            };

            using (var db = new UserContext())
            {
                db.Users.Add(userData);
                db.SaveChanges();
            }

            return new ResponseMsg
            {
                IsSuccess = true,
                Message = "User created successfully",
            };
        }

        protected ResponseMsg ExecuteUpdateUserAction(UserDto userDto)
        {
            using (var db = new UserContext())
            {
                var existingUser = db.Users.FirstOrDefault(u => u.Id == userDto.Id);

                if (existingUser == null) 
                {
                    return new ResponseMsg
                    {
                        IsSuccess = false,
                        Message = "User not found"
                    };
                }

                existingUser.Username = userDto.Username;
                existingUser.Email = userDto.Email;

                db.SaveChanges();
            }

            return new ResponseMsg
            {
                IsSuccess = true,
                Message = "User data changed successfully"
            };
        }

        protected ResponseMsg ExecuteDeleteUserAction(int id)
        {
            using (var db = new UserContext())
            {
                var existingUser = db.Users.FirstOrDefault(u => u.Id == id);

                if (existingUser == null)
                {
                    return new ResponseMsg
                    {
                        IsSuccess = false,
                        Message = "User was not found"
                    };
                }

                existingUser.IsActive = false; //I'm gonna deactivate it for now, maybe later I will make full delete
                db.SaveChanges();

                return new ResponseMsg
                {
                    IsSuccess = true,
                    Message = "User was deactivated"
                };

            }
        }

        protected ResponseMsg ExecuteLoginAction(UserAuthDto user)
        {
            if (string.IsNullOrWhiteSpace(user.Email))
            {
                return new ResponseMsg
                {
                    IsSuccess = false,
                    Message  = "Please, enter the email"
                };
            }

            if (string.IsNullOrWhiteSpace(user.Password))
            {
                return new ResponseMsg
                {
                    IsSuccess = false,
                    Message = "Please, enter the password"
                };
            }

            using (var db = new UserContext())
            {
                var email = user.Email.ToLower();
                var existingUser = db.Users.FirstOrDefault(u => u.Email.ToLower() == email);

                if (existingUser == null)
                {
                    return new ResponseMsg
                    {
                        IsSuccess = false,
                        Message = "Incorrect data, please try again!"
                    };
                }

                if (!VerifyPassword(user.Password, existingUser.PasswordHash))
                {
                    return new ResponseMsg
                    {
                        IsSuccess = false,
                        Message = "Incorrect data, please try again!"
                    };
                }
            }

            return new ResponseMsg
            {
                IsSuccess = true,
                Message = "Login Successfull"
            };
        }
        
    }
}
