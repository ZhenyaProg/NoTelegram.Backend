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
        private readonly IMessagesService _messagesService;

        public ChatsService(IUsersService usersService, IChatsRepository chatsRepository, IMessagesService messagesService)
        {
            _usersService = usersService;
            _chatsRepository = chatsRepository;
            _messagesService = messagesService;
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

        public async Task<Result> SendMessage(Guid senderId, Guid chatId, string messageType, string messageText)
        {
            var findSenderResult = await _usersService.GetBySecurityId(senderId);
            if (findSenderResult.IsFailure) return findSenderResult;

            Chats? chat = await _chatsRepository.GetById(chatId);
            if (chat is null) return Result.Failure($"Нет чата с id: {chatId}");
            //TODO: проверка прав и доступа

            Messages message = await _messagesService.Create(senderId, chatId, messageType, messageText);
            
            return Result.Success();
        }

        public async Task<Result<List<Messages>>> ReadMessages(Guid readerId, Guid chatId, int pageNumber, int pageSize)
        {
            var findSenderResult = await _usersService.GetBySecurityId(readerId);
            if (findSenderResult.IsFailure) return Result.Failure<List<Messages>>(findSenderResult.Error);

            Chats? chat = await _chatsRepository.GetById(chatId);
            if (chat is null) return Result.Failure<List<Messages>>($"Нет чата с id: {chatId}");
            //TODO: проверка прав и доступа
            //TODO: пагинация
            return Result.Success(chat.Messages);
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
    }
}