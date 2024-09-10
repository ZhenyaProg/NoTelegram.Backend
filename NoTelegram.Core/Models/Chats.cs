﻿namespace NoTelegram.Core.Models
{
    public class Chats
    {
        public Chats(Guid chatId, Guid creatorId, ChatType type, string chatName, AccessType chatAccess, List<Guid> interlocutors)
        {
            ChatId = chatId;
            CreatorId = creatorId;
            Type = type;
            ChatName = chatName;
            ChatAccess = chatAccess;
            Interlocutors = interlocutors;
        }

        public Guid ChatId { get; private set; }

        public Guid CreatorId { get; private set; }
        public string ChatName { get; private set; } = string.Empty;
        public AccessType ChatAccess { get; private set; }
        public ChatType Type { get; private set; }

        public List<Guid> Interlocutors { get; private set; } = new List<Guid>();
    }

    public enum AccessType
    {
        None,
        FullBlock,
        PieceBlock,
        Free
    }

    public enum ChatType
    {
        None,
        Personally,
        Group,
        Channel
    }
}