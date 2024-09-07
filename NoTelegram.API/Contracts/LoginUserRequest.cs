using NoTelegram.Core.Models;

namespace NoTelegram.API.Contracts
{
    public class LoginUserRequest
    {
        public string LoginType { get; set; } = string.Empty;
        public string Login { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}