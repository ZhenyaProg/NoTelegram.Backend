namespace NoTelegram.Core.Models
{
    public class Users
    {
        public const int MAX_USER_NAME_LENGTH = 15;
        public const int MAX_PASSWORD_LENGTH = 6;

        public Users(Guid id, string userName, string password, string email)
        {
            Id = id;
            UserName = userName;
            PasswordHashed = password;
            Email = email;
        }

        public Guid Id { get; private set; }
        public string UserName { get; private set; } = string.Empty;
        public string PasswordHashed { get; private set; } = string.Empty;
        public string Email { get; private set; } = string.Empty;
    }
}