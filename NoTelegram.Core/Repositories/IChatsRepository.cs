using NoTelegram.Core.Models;

namespace NoTelegram.Core.Repositories
{
    public interface IChatsRepository
    {
        Task Add(Chats chats);
        Task<Chats?> GetById(Guid id);
    }
}