namespace NoTelegram.Core.Models
{
    public class Messages
    {
        public Messages(Guid messageId, MessageType messageType, string messageText)
        {
            MessageId = messageId;
            MessageType = messageType;
            MessageText = messageText;
        }

        public Guid MessageId { get; private set; }
        public MessageType MessageType { get; private set; }
        public string MessageText { get; private set; } = string.Empty;
    }

    public enum MessageType
    {
        None,
        Text,
        Photo,
        Video
    }
}