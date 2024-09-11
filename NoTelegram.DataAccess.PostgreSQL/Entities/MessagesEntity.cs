using NoTelegram.Core.Models;

namespace NoTelegram.DataAccess.PostgreSQL.Entities
{
    public class MessagesEntity
    {
        public Guid MessageId { get; set; }
        public MessageType MessageType { get; set; }
        public string MessageText { get; set; } = string.Empty;

        public Guid ChatId { get; set; }
        public ChatsEntity? Chat { get; set; }
    }
}