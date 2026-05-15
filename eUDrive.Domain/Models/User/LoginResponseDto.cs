
namespace eUDrive.Domains.Models.User
{
    public class LoginResponseDto
    {
        public int UserId { get; set; }
        public string Token { get; set; } = string.Empty;
    }
}
