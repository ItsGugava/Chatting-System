using Chatting_System.Dtos.GroupMessage;
using Chatting_System.Models;

namespace Chatting_System.Mappers
{
    public static class GroupMessageMapper
    {
        public static GroupMessage FromCreateDtoToGroupMessage(this CreateGroupMessageRequestDto groupMessageDto, string senderId)
        {
            return new GroupMessage
            {
                Content = groupMessageDto.Content,
                SenderId = senderId,
                GroupId = groupMessageDto.GroupId
            };
        }

        public static GroupMessageDto FromGroupMessageToGroupMessageDto(this GroupMessage groupMessage)
        {
            return new GroupMessageDto
            {
                Id = groupMessage.Id,
                SenderId = groupMessage.SenderId,
                GroupId = groupMessage.GroupId,
                Content = groupMessage.Content,
                DateTime = groupMessage.DateTime
            };
        }
    }
}
