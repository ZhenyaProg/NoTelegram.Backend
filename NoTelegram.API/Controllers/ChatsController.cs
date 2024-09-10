using Microsoft.AspNetCore.Mvc;
using NoTelegram.API.Contracts.Chat;
using NoTelegram.API.Filters.Auth;
using NoTelegram.Core.Services;

namespace NoTelegram.API.Controllers
{
    [Route("api/chats")]
    [ApiController]
    public class ChatsController : ControllerBase
    {
        private readonly IChatsService _chatsService;

        public ChatsController(
            IChatsService chatsService)
        {
            _chatsService = chatsService;
        }

        [HttpPost]
        [Route("personallyChat")]
        [TypeFilter(typeof(AuthWithTime))]
        public async Task<IResult> CreatePersonallyChat([FromHeader] Guid securityId,
                                                        [FromBody] Guid interlocutorId)
        {
            var createResult = await _chatsService.CreatePersonally(securityId, interlocutorId);
            if (createResult.IsFailure)
                return Results.BadRequest(createResult.Error);

            return Results.Created();
        }

        [HttpPost]
        [Route("groupChat")]
        [TypeFilter(typeof(AuthWithTime))]
        public async Task<IResult> CreateGroupChat([FromHeader] Guid securityId,
                                                    [FromBody] CreateGroupChatRequest request)
        {
            var createResult = await _chatsService.CreateGroup(securityId, request.InterlocutorsId, request.ChatName, request.AccessType);
            if (createResult.IsFailure)
                return Results.BadRequest(createResult.Error);

            return Results.Ok();
        }

        [HttpPost]
        [Route("channel")]
        [TypeFilter(typeof(AuthWithTime))]
        public async Task<IResult> CreateChannel([FromHeader] Guid securityId,
                                                 [FromBody] CreateChannelRequest request)
        {
            var createResult = await _chatsService.CreateChannel(securityId, request.ChatName, request.AccessType);
            if (createResult.IsFailure)
                return Results.BadRequest(createResult.Error);

            return Results.Ok();
        }

        [HttpPost]
        [Route("messages")]
        [TypeFilter(typeof(AuthWithTime))]
        public async Task<IResult> SendMessage([FromHeader] Guid securityId,
                                               [FromBody] SendMessageRequest request)
        {
            await _chatsService.SendMessage(securityId, request.ChatId, request.MessageType, request.Message);

            return Results.Ok();
        }

        [HttpGet]
        [Route("messages")]
        [TypeFilter(typeof(AuthWithTime))]
        public async Task<IResult> ReadMessages([FromHeader] Guid securityId,
                                                [FromBody] ReadMessagesRequest request)
        {
            await _chatsService.ReadMessages(securityId, request.ChatId, request.PageNumber, request.PageSize);

            ReadMessagesResponse response = new ReadMessagesResponse();

            return Results.Ok(response);
        }
    }
}