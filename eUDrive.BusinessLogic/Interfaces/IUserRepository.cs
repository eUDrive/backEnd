using eUDrive.Domains.Entities;

namespace eUDrive.BusinessLogic.Interfaces
{
    public interface IUserRepository
    {
        Task<UserData?> GetByIdAsync(int id);
        Task<IEnumerable<UserData>> GetAllAsync();
        Task<UserData?> GetByUsernameAsync(string username);
        Task AddAsync(UserData user);
        Task UpdateAsync(UserData user);
        Task DeleteAsync(UserData user);
    }
}
