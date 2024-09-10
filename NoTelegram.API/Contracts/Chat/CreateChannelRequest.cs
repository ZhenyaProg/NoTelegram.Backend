namespace NoTelegram.API.Contracts.Chat
{
    public class CreateChannelRequest
    {
        public string ChatName { get; set; } = string.Empty;
        public string AccessType { get; set; } = string.Empty;
    }
}