using CSharpFunctionalExtensions;
using NoTelegram.Core.Models;
using NoTelegram.Core.Repositories;
using NoTelegram.Core.Services;

namespace NoTelegram.Application.Services
{
    public class ChatsService : IChatsService
    {
        private readonly IUsersService _usersService;
        private readonly IChatsRepository _chatsRepository;

        public ChatsService(IUsersService usersService, IChatsRepository chatsRepository)
        {
            _usersService = usersService;
            _chatsRepository = chatsRepository;
        }

        public async Task<Result> CreatePersonally(Guid creatorId, Guid interlocutorId)
        {
            var findCreatorResult = await _usersService.GetBySecurityId(creatorId);
            if (findCreatorResult.IsFailure) return findCreatorResult;
            var findInterlocutorResult = await _usersService.GetBySecurityId(interlocutorId);
            if (findInterlocutorResult.IsFailure) return findInterlocutorResult;

            Chats chats = new Chats(Guid.NewGuid(),
                                    creatorId,
                                    ChatType.Personally,
                                    $"{findCreatorResult.Value.UserName}/{findInterlocutorResult.Value.UserName}",
                                    AccessType.FullBlock,
                                    new List<Guid>([creatorId, interlocutorId]));

            await _chatsRepository.Add(chats);

            return Result.Success();
        }

        public async Task<Result> CreateGroup(Guid creatorId, Guid[] interlocutorsId, string chatName, string accessType)
        {
            var findCreatorResult = await _usersService.GetBySecurityId(creatorId);
            if (findCreatorResult.IsFailure) return findCreatorResult;
            foreach (var interlocutorId in interlocutorsId)
            {
                var findInterlocutorResult = await _usersService.GetBySecurityId(interlocutorId);
                if (findInterlocutorResult.IsFailure) return findInterlocutorResult;
            }

            List<Guid> interlocutors = new List<Guid>(interlocutorsId);
            interlocutors.Add(creatorId);

            Chats chats = new Chats(Guid.NewGuid(),
                                   creatorId,
                                   ChatType.Group,
                                   chatName,
                                   GetAccessType(accessType),
                                   interlocutors);

            await _chatsRepository.Add(chats);

            return Result.Success();
        }

        public async Task<Result> CreateChannel(Guid creatorId, string chatName, string accessType)
        {
            var findCreatorResult = await _usersService.GetBySecurityId(creatorId);
            if (findCreatorResult.IsFailure) return findCreatorResult;

            Chats chats = new Chats(Guid.NewGuid(),
                                    creatorId,
                                    ChatType.Channel,
                                    chatName,
                                    GetAccessType(accessType),
                                    new List<Guid>([creatorId]));

            await _chatsRepository.Add(chats);

            return Result.Success();
        }

        public async Task<Result> SendMessage(Guid senderId, Guid chatId, string messageType, string message)
        {
            var findSenderResult = await _usersService.GetBySecurityId(senderId);
            if (findSenderResult.IsFailure) return findSenderResult;

            Chats? chat = await _chatsRepository.GetById(chatId);
            if (chat is null) return Result.Failure($"Нет чата с id: {chatId}");



            return Result.Success();
        }

        public Task ReadMessages(Guid securityId, Guid chatId, int pageNumber, int pageSize)
        {
            throw new NotImplementedException();
        }

        private AccessType GetAccessType(string accessType)
        {
            return accessType switch
            {
                "fb" => AccessType.FullBlock,
                "pb" => AccessType.PieceBlock,
                "f" => AccessType.Free,
                _ => throw new InvalidDataException($"{accessType} is not {nameof(AccessType)}")
            };
        }

        private MessageType GetMessageType(string messageType)
        {
            return messageType switch
            {
                "text" => MessageType.Text,
                "photo" => MessageType.Photo,
                "video" => MessageType.Video,
                _ => throw new InvalidDataException($"{messageType} is not {nameof(MessageType)}")
            };
        }
    }
}