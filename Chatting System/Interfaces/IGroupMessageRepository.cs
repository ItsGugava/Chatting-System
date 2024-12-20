using Chatting_System.Models;

namespace Chatting_System.Interfaces
{
    public interface IGroupMessageRepository
    {
        Task<GroupMessage?> CreateAsync(GroupMessage groupMessage);
        Task<GroupMessage?> DeleteAsync(int id, string appUserId);
        Task<List<GroupMessage>> GetAllAsync(int groupId, string appUserId);
    }
}
