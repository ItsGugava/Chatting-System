using Chatting_System.Dtos.Group;
using Chatting_System.Models;

namespace Chatting_System.Interfaces
{
    public interface IGroupRepository
    {
        Task<GroupMember?> AddUserAsync(int id, AddUserRequestDto addUserRequestDto, string appUserId);
        Task<Group?> CreateAsync(CreateGroupRequestDto groupDto);
        Task<Group?> DeleteAsync(int id, string appUserId);
        Task<List<GroupMemberDto>> GetGroupMembersAsync(int id, string appUserId);
        List<Group> GetMyGroups(string appUserId);
        Task<GroupMember?> LeaveGroupAsync(int id, string appUserId);
        Task<GroupMember?> RemoveUserAsync(int id, RemoveUserRequestDto removeUserRequestDto, string appUserId);
        Task<Group?> UpdateAsync(int id, UpdateGroupRequestDto groupDto, string appUserId);
    }
}
