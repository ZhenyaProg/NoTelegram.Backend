namespace NoTelegram.API.Contracts
{
    public class UserDataResponse
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public bool Authenticated { get; set; }
    }
}