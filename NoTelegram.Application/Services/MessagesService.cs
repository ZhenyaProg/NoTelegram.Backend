using NoTelegram.Core.Models;
using NoTelegram.Core.Repositories;
using NoTelegram.Core.Services;

namespace NoTelegram.Application.Services
{
    public class MessagesService : IMessagesService
    {
        private readonly IMessagesRepository _messagesRepository;

        public MessagesService(IMessagesRepository messagesRepository)
        {
            _messagesRepository = messagesRepository;
        }

        public async Task<Messages> Create(Guid senderId, Guid chatId, string messageType, string messageText)
        {
            Messages message = new Messages(Guid.NewGuid(),
                                            chatId,
                                            GetMessageType(messageType),
                                            messageText);

            await _messagesRepository.Add(message);

            return message;
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