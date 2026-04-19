

namespace eUDrive.BusinessLogic.Interfaces
{
    public interface ISessionActions
    {
        string CreateOrUpdateSession(int userId);
        int? GetUserIdByCookie(string sessionKey);
        void DeleteSession(string sessionKey);
    }
}
