using Chatting_System.Dtos.RegisteredUsers;

namespace Chatting_System.Interfaces
{
    public interface IRegisteredUsersRepository
    {
        Task<List<RegisteredUsersDto>> GetAllAsync(string appUserId);
    }
}
