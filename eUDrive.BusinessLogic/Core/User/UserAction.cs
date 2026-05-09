using eUDrive.BusinessLogic.Structure;
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

            if (string.IsNullOrWhiteSpace(userDto.Password) || userDto.Password.Length < 8)
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
            }

            var userData = new UserData()
            {
                Username = userDto.Username,
                Email = userDto.Email,
                PasswordHash = HashPassword(userDto.Password),
                CreatedAt = DateTime.Now,
                IsActive = true,
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

        protected ResponseMsg ExecuteUpdateUserAction(UserUpdateDto userUpdateDto)
        {
            using (var db = new UserContext())
            {
                var existingUser = db.Users.FirstOrDefault(u => u.Id == userUpdateDto.Id);

                if (existingUser == null) 
                {
                    return new ResponseMsg
                    {
                        IsSuccess = false,
                        Message = "User not found"
                    };
                }

                if (string.IsNullOrWhiteSpace(userUpdateDto.Username))
                {
                    return new ResponseMsg
                    {
                        IsSuccess = false,
                        Message = "Username shouldn't be empty. "
                    };
                }

                if (string.IsNullOrWhiteSpace(userUpdateDto.Email) || !userUpdateDto.Email.Contains("@"))
                {
                    return new ResponseMsg
                    {
                        IsSuccess = false,
                        Message = "Invalid Email format"
                    };
                }

                var email = userUpdateDto.Email.ToLower();

                var existingUserByEmail = db.Users.FirstOrDefault(u => u.Id != userUpdateDto.Id && u.Email.ToLower() == email);

                if (existingUserByEmail != null)
                {
                    return new ResponseMsg
                    {
                        IsSuccess = false,
                        Message = "This email already exists"
                    };
                }

                existingUser.Username = userUpdateDto.Username;
                existingUser.Email = userUpdateDto.Email;
                existingUser.IsActive = userUpdateDto.IsActive;

                db.SaveChanges();
            }

            return new ResponseMsg
            {
                IsSuccess = true,
                Message = "User data changed successfully"
            };
        }

        protected ResponseMsg ExecuteActivateUserAction(UserActivateDto user)
        {
            using (var db = new UserContext())
            {
                var existingUser = db.Users.FirstOrDefault(u => u.Id == user.Id);

                if (existingUser == null )
                {
                    return new ResponseMsg
                    {
                        IsSuccess = false,
                        Message = "This user doesn't exist"
                    };
                }

                existingUser.IsActive = user.IsActive;

                db.SaveChanges();

                return new ResponseMsg
                {
                    IsSuccess = true,
                    Message = "Activation has been completed"
                };
            }
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

        protected ResponseAction<LoginResponseDto> ExecuteLoginAction(UserAuthDto user)
        {
            if (string.IsNullOrWhiteSpace(user.Email))
            {
                return new ResponseAction<LoginResponseDto>
                {
                    IsSuccess = false,
                    Message  = "Please, enter the email",
                    Data = null
                };
            }

            if (string.IsNullOrWhiteSpace(user.Password))
            {
                return new ResponseAction<LoginResponseDto>
                {
                    IsSuccess = false,
                    Message = "Please, enter the password",
                    Data = null
                };
            }

            var existingUser = new UserData(); 

            using (var db = new UserContext())
            {
                var email = user.Email.ToLower();
                existingUser = db.Users.FirstOrDefault(u => u.Email.ToLower() == email);

                if (existingUser == null)
                {
                    return new ResponseAction<LoginResponseDto>
                    {
                        IsSuccess = false,
                        Message = "Incorrect data, please try again!",
                        Data = null
                    };
                }

                if (!VerifyPassword(user.Password, existingUser.PasswordHash))
                {
                    return new ResponseAction<LoginResponseDto>
                    {
                        IsSuccess = false,
                        Message = "Incorrect data, please try again!",
                        Data = null
                    };
                }
            }

            var token = GenerateUserToken(existingUser);

            return new ResponseAction<LoginResponseDto>
            {
                IsSuccess = true,
                Message = "Login Successfull",
                Data = new LoginResponseDto
                {
                    UserId = existingUser.Id,
                    Token = token
                }
            };
        }

        internal string GenerateUserToken(UserData user)
        {
            var token = new TokenService();
            return token.GenerateToken(user.Id, user.Username, user.Role.ToString());
        }

    }
}
