using Chatting_System.Dtos.UserMessage;
using Chatting_System.Models;

namespace Chatting_System.Interfaces
{
    public interface IUserMessageRepository
    {
        Task CreateAsync(UserMessage userMessage);
        Task<UserMessage?> UpdateAsync(int id, UpdateUserMessageRequestDto updateUserMessageDto, string appUserId);
        Task<UserMessage?> DeleteAsync(int id, string appUserId);
        Task<List<UserMessage>> GetUserMessagesAsync(string authorizedUserId, string otherUserId);
    }
}
