using Microsoft.EntityFrameworkCore;
using NoTelegram.Core.Models;
using NoTelegram.Core.Repositories;
using NoTelegram.DataAccess.PostgreSQL.Entities;

namespace NoTelegram.DataAccess.PostgreSQL.Repositories
{
    public class MessagesRepository : IMessagesRepository
    {
        private readonly DataBaseContext _dbContext;

        public MessagesRepository(DataBaseContext dbContext, IChatsRepository chatsRepository)
        {
            _dbContext = dbContext;
        }

        public async Task Add(Messages message)
        {
            MessagesEntity messagesEntity = new MessagesEntity
            {
                MessageId = message.MessageId,
                ChatId = message.ChatId,
                MessageType = message.MessageType,
                MessageText = message.MessageText,
            };

            var chat = await _dbContext.Chats.FirstAsync(chat => chat.ChatId == message.ChatId);
            chat.Messages.Add(messagesEntity);

            await _dbContext.SaveChangesAsync();
        }
    }
}