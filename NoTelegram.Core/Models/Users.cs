namespace NoTelegram.Core.Models
{
    public class Users
    {
        public const int MAX_USER_NAME_LENGTH = 15;
        public const int MAX_PASSWORD_LENGTH = 6;

        public Guid SecurityId { get; set; }
        public Guid UserId { get; set; }

        public string UserName { get; set; } = string.Empty;
        public string PasswordHashed { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        public bool Authenticated { get; set; }
        public DateTime AuthDate { get; set; }
    }
}