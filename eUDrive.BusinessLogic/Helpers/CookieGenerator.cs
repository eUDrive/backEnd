using System.Security.Cryptography;

namespace eUDrive.BusinessLogic.Helpers
{
    public class CookieGenerator
    {
        public static string Create()
        {
            var bytes = RandomNumberGenerator.GetBytes(32);
            return Convert.ToBase64String(bytes);
        }
    }
}
