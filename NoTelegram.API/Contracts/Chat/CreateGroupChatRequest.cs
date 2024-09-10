namespace NoTelegram.API.Contracts.Chat
{
    public class CreateGroupChatRequest
    {
        public Guid[] InterlocutorsId { get; set; } = [];
        public string ChatName { get; set; } = string.Empty;
        public string AccessType { get; set; } = string.Empty;
    }
}