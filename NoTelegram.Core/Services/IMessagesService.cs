using NoTelegram.Core.Models;

namespace NoTelegram.Core.Services
{
    public interface IMessagesService
    {
        Task<Messages> Create(Guid senderId, Guid chatId, string messageType, string messageText);
    }
}