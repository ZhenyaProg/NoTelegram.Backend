namespace NoTelegram.API.Contracts.Chat
{
    public class SendMessageRequest
    {
        public Guid ChatId { get; set; }
        public string MessageType { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
    }
}