namespace NoTelegram.DataAccess.PostgreSQL.Entities
{
    public class UsersEntity
    {
        public Guid SecurityId { get; set; }
        public Guid UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public bool Authenticated { get; set; }
        public DateTime AuthDate { get; set; }
    }
}