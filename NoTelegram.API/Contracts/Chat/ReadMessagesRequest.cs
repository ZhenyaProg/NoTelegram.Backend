namespace NoTelegram.API.Contracts.Chat
{
    public class ReadMessagesRequest
    {
        public Guid ChatId { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}