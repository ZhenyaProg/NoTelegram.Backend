namespace NoTelegram.DataAccess.PostgreSQL.Entities
{
    public class UsersEntity
    {
        public Guid Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}