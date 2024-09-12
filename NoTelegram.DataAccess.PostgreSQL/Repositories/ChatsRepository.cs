using Microsoft.EntityFrameworkCore;
using NoTelegram.Core.Models;
using NoTelegram.Core.Repositories;
using NoTelegram.DataAccess.PostgreSQL.Entities;

namespace NoTelegram.DataAccess.PostgreSQL.Repositories
{
    public class ChatsRepository : IChatsRepository
    {
        private readonly DataBaseContext _dbContext;

        public ChatsRepository(DataBaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Add(Chats chats)
        {
            ChatsEntity chatsEntity = new ChatsEntity
            {
                ChatId = chats.ChatId,
                Type = chats.Type,
                CreatorId = chats.CreatorId,
                ChatName = chats.ChatName,
                ChatAccess = chats.ChatAccess,
            };

            await _dbContext.Chats.AddAsync(chatsEntity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Chats?> GetById(Guid id)
        {
            ChatsEntity? chatsEntity = await _dbContext.Chats
                .AsNoTracking()
                .Include(chat => chat.Messages)
                .Include(chat => chat.Interlocutors)
                .FirstOrDefaultAsync(chat => chat.ChatId == id);
            if(chatsEntity is null) return null;

            return new Chats(id, chatsEntity.CreatorId, chatsEntity.Type, chatsEntity.ChatName, chatsEntity.ChatAccess,
                             chatsEntity.Interlocutors.Select(s => s.SecurityId).ToList(),
                             chatsEntity.Messages.Select(entity => new Messages(entity.MessageId, 
                                                                                entity.ChatId,
                                                                                entity.MessageType,
                                                                                entity.MessageText)).ToList());
        }
    }
}