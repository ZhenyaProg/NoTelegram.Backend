
using CSharpFunctionalExtensions;
using NoTelegram.Core.Models;

namespace NoTelegram.Core.Services
{
    public interface IChatsService
    {
        Task<Result> CreatePersonally(Guid creatorId, Guid interlocutorId);
        Task<Result> CreateGroup(Guid creatorId, Guid[] interlocutorsId, string name, string accessType);
        Task<Result> CreateChannel(Guid creatorId, string chatName, string accesType);
        Task<Result> SendMessage(Guid securityId, Guid chatId, string messageType, string message);
        Task<Result<List<Messages>>> ReadMessages(Guid securityId, Guid chatId, int pageNumber, int pageSize);
    }
}