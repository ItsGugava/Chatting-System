using Chatting_System.Data;
using Chatting_System.Dtos.Group;
using Chatting_System.Interfaces;
using Chatting_System.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Chatting_System.Repositories
{
    public class GroupRepository : IGroupRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public GroupRepository(ApplicationDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<GroupMember?> AddUserAsync(int id, AddUserRequestDto addUserRequestDto, string appUserId)
        {
            if(!_context.GroupMembers.Any(gm => gm.GroupId == id && gm.AppUserId.Equals(appUserId)))
            {
                return null;
            }
            if(!_userManager.Users.Any(u => u.Id.Equals(addUserRequestDto.AppUserId)))
            {
                return null;
            }
            if(_context.GroupMembers.Any(gm => gm.GroupId == id && gm.AppUserId.Equals(addUserRequestDto.AppUserId)))
            {
                return null;
            }
            GroupMember groupMember = new GroupMember { GroupId = id, AppUserId = addUserRequestDto.AppUserId };
            await _context.GroupMembers.AddAsync(groupMember);
            await _context.SaveChangesAsync();
            return groupMember;
        }

        public async Task<Group?> CreateAsync(CreateGroupRequestDto groupDto)
        {
            foreach (var appUserId in groupDto.AppUserIds)
            {
                if(!_userManager.Users.Any(u => u.Id.Equals(appUserId)))
                { 
                    return null;
                }   
            }
            Group group = new Group { Name = groupDto.Name };
            await _context.Groups.AddAsync(group);
            await _context.SaveChangesAsync();
            List<GroupMember> groupMembers = groupDto.AppUserIds.Distinct().Select(u => new GroupMember { GroupId = group.Id, AppUserId = u}).ToList();
            await _context.GroupMembers.AddRangeAsync(groupMembers);
            await _context.SaveChangesAsync();
            return group;
        }

        public async Task<Group?> DeleteAsync(int id, string appUserId)
        {
            Group? group = _context.Groups.FirstOrDefault(u => u.Id == id);
            if(group == null)
            {
                return null;
            }
            if(!_context.GroupMembers.Any(gm => gm.GroupId == id && gm.AppUserId.Equals(appUserId)))
            {
                return null;
            }
            _context.Groups.Remove(group);
            await _context.SaveChangesAsync();
            return group;
        }

        public async Task<List<GroupMemberDto>> GetGroupMembersAsync(int id, string appUserId)
        {
            if(!_context.GroupMembers.Any(gm => gm.GroupId == id && gm.AppUserId.Equals(appUserId)))
            {
                return new List<GroupMemberDto> { };
            }
            List<GroupMember> groupMembers = await _context.GroupMembers.Include(gm => gm.AppUser).Where(gm => gm.GroupId == id).ToListAsync();
            List<GroupMemberDto> groupMemberDtos = groupMembers.Select(gm => new GroupMemberDto { Id = gm.AppUser.Id, UserName = gm.AppUser.UserName }).ToList();
            return groupMemberDtos;
        }

        public List<Group> GetMyGroups(string appUserId)
        {
            List<GroupMember> joinedGroupsMember = _context.GroupMembers.Include(gm => gm.Group).Where(gm => gm.AppUserId.Equals(appUserId)).ToList(); 
            List<Group> myGroups = joinedGroupsMember.Select(gm => gm.Group).ToList();
            return myGroups;
        }

        public async Task<GroupMember?> LeaveGroupAsync(int id, string appUserId)
        {
            GroupMember? groupMember = _context.GroupMembers.FirstOrDefault(gm => gm.GroupId == id && gm.AppUserId.Equals(appUserId));
            if(groupMember == null)
            {
                return null;
            }
            _context.GroupMembers.Remove(groupMember);
            await _context.SaveChangesAsync();
            return groupMember;
        }

        public async Task<GroupMember?> RemoveUserAsync(int id, RemoveUserRequestDto removeUserRequestDto, string appUserId)
        {
            if(!_context.GroupMembers.Any(gm => gm.GroupId == id && gm.AppUserId.Equals(appUserId)))
            {
                return null;
            }
            GroupMember? groupMember = _context.GroupMembers.FirstOrDefault(gm => gm.GroupId == id && gm.AppUserId.Equals(removeUserRequestDto.AppUserId));
            if(groupMember == null)
            {
                return null;
            }
            _context.GroupMembers.Remove(groupMember);
            await _context.SaveChangesAsync();
            return groupMember;
        }

        public async Task<Group?> UpdateAsync(int id, UpdateGroupRequestDto groupDto, string appUserId)
        {
            Group? group = _context.Groups.FirstOrDefault(g =>  g.Id == id);
            if(group == null)
            {
                return null;
            }
            if(!_context.GroupMembers.Any(gm => gm.GroupId == group.Id && gm.AppUserId.Equals(appUserId)))
            {
                return null;
            }
            group.Name = groupDto.Name;
            await _context.SaveChangesAsync();
            return group;
        }
    }
}
