using NoTelegram.Core.Models;

namespace NoTelegram.DataAccess.PostgreSQL.Entities
{
    public class ChatsEntity
    {
        public Guid ChatId { get; set; }

        public Guid CreatorId { get; set; }
        public string ChatName { get; set; } = string.Empty;
        public AccessType ChatAccess { get; set; }
        public ChatType Type { get; set; }

        public List<UsersEntity> Interlocutors { get; set; } = [];
    }
}