using eUDrive.BusinessLogic.Helpers;
using eUDrive.DataAccess.Context;
using eUDrive.Domains.Entities.Session;

namespace eUDrive.BusinessLogic.Core.Session
{
    public class SessionAction
    {
        protected string ExecuteCreateOrUpdateSession(int userId)
        {
            var sessionKey = CookieGenerator.Create();
            var expiresAt = DateTime.Now.AddMinutes(60);

            using (var db = new SessionContext())
            {
                var current = db.Sessions.FirstOrDefault(s => s.UserId == userId);

                if (current != null)
                {
                    current.SessionKey = sessionKey;
                    current.ExpiresAt = expiresAt;
                    db.SaveChanges();
                }
                else
                {
                    db.Sessions.Add(new SessionData { UserId = userId, SessionKey = sessionKey, ExpiresAt = expiresAt });
                    db.SaveChanges();
                }
            }
            return sessionKey;
        }

        protected int? ExecuteGetUserIdByCookie(string sessionKey)
        {
            if (string.IsNullOrWhiteSpace(sessionKey))
            {
                return null;
            }

            using (var db = new SessionContext())
            {
                var session = db.Sessions.FirstOrDefault(s => s.SessionKey == sessionKey);
                if (session == null) return null;

                if (session.ExpiresAt <= DateTime.Now) return null;

                return session.UserId;
            }
        }

        protected void ExecuteDeleteSession(string sessionKey)
        {
            if (string.IsNullOrWhiteSpace(sessionKey)) return;

            using (var db = new SessionContext())
            {
                var session = db.Sessions.FirstOrDefault(s => s.SessionKey == sessionKey);

                if (session != null)
                {
                    db.Sessions.Remove(session);
                    db.SaveChanges();
                }
            }
        }
    }
}
