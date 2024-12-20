using Chatting_System.Dtos.UserMessage;
using Chatting_System.Models;

namespace Chatting_System.Mappers
{
    public static class UserMessageMapper
    {
        public static UserMessage FromCreateDtoToUserMessage(this CreateUserMessageRequestDto createDto, string senderId)
        {
            return new UserMessage
            {
                Content = createDto.Content,
                SenderId = senderId,
                ReceiverId = createDto.ReceiverId
            };
        }
        public static UserMessageDto FromUserMessageToUserMessageDto(this UserMessage userMessage)
        {
            return new UserMessageDto
            {
                Id = userMessage.Id,
                Content = userMessage.Content,
                SenderId = userMessage.SenderId,
                ReceiverId = userMessage.ReceiverId,
                DateTime = userMessage.DateTime
            };
        }
    }
}
