using System.Text;
using eUDrive.BusinessLogic.Interfaces;
using eUDrive.Domains.Entities;

namespace eUDrive.BusinessLogic.Functions
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserData?> GetByIdAsync(int id)
        {
            return await _userRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<UserData>> GetAllAsync()
        {
            return await _userRepository.GetAllAsync();
        }

        public async Task<UserData?> GetByUsernameAsync(string username)
        {
            return await _userRepository.GetByUsernameAsync(username);
        }

        public async Task RegisterAsync(string name, string email, string password)
        {
            //Username should be unique
            var existingUser = await _userRepository.GetByUsernameAsync(name);
            if (existingUser != null)
            {
                throw new Exception("Username already exists");
            }

            var user = new UserData
            {
                Username = name,
                Email = email,
                password = password,
                CreatedAt = DateTime.UtcNow,
                IsActive = true
            };

            await _userRepository.AddAsync(user);
        }

        public async Task UpdateAsync(int id, string name, string email, string password)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
            {
                throw new KeyNotFoundException($"User by id {id} not found");
            }

            user.Username = name;
            user.Email = email;
            user.password = password;

            await _userRepository.UpdateAsync(user);
        }

        public async Task DeleteAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
            {
                throw new KeyNotFoundException($"User by id {id} not found");
            }

            await _userRepository.DeleteAsync(user);
        }
    }
}
