using NoTelegram.Core.Models;

namespace NoTelegram.Core.Repositories
{
    public interface IMessagesRepository
    {
        Task Add(Messages message);
    }
}