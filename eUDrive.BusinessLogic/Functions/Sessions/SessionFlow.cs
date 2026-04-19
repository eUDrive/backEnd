using eUDrive.BusinessLogic.Core.Session;
using eUDrive.BusinessLogic.Interfaces;

namespace eUDrive.BusinessLogic.Functions.Sessions
{
    public class SessionFlow: SessionAction, ISessionActions
    {
        public string CreateOrUpdateSession(int userId)
        {
            return ExecuteCreateOrUpdateSession(userId);
        }

        public int? GetUserIdByCookie(string sessionKey)
        {
            return ExecuteGetUserIdByCookie(sessionKey);
        }

        public void DeleteSession(string sessionKey)
        {
            ExecuteDeleteSession(sessionKey);
        }
    }
}
