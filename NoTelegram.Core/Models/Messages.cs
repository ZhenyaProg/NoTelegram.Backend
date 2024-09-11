namespace NoTelegram.Core.Models
{
    public class Messages
    {
        public Messages(Guid messageId, Guid chatId, MessageType messageType, string messageText)
        {
            MessageId = messageId;
            ChatId = chatId;
            MessageType = messageType;
            MessageText = messageText;
        }

        public Guid MessageId { get; private set; }
        public MessageType MessageType { get; private set; }
        public string MessageText { get; private set; } = string.Empty;

        public Guid ChatId { get; private set; }
    }

    public enum MessageType
    {
        None,
        Text,
        Photo,
        Video
    }
}