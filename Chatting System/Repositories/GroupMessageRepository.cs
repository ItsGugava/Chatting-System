using Chatting_System.Data;
using Chatting_System.Interfaces;
using Chatting_System.Models;
using Microsoft.EntityFrameworkCore;

namespace Chatting_System.Repositories
{
    public class GroupMessageRepository : IGroupMessageRepository
    {
        private readonly ApplicationDbContext _context;

        public GroupMessageRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<GroupMessage?> CreateAsync(GroupMessage groupMessage)
        {
            if(!_context.GroupMembers.Any(gm => gm.GroupId == groupMessage.GroupId && gm.AppUserId.Equals(groupMessage.SenderId)))
            {
                return null;
            }
            await _context.GroupMessages.AddAsync(groupMessage);
            await _context.SaveChangesAsync();
            return groupMessage;
        }

        public async Task<GroupMessage?> DeleteAsync(int id, string appUserId)
        {
            GroupMessage? groupMessage = await _context.GroupMessages.FirstOrDefaultAsync(gm => gm.Id == id && gm.SenderId.Equals(appUserId));
            if (groupMessage == null)
            {
                return null;
            }
            _context.GroupMessages.Remove(groupMessage);
            await _context.SaveChangesAsync();
            return groupMessage;
        }

        public async Task<List<GroupMessage>> GetAllAsync(int groupId, string appUserId)
        {
            if(!_context.GroupMembers.Any(gm => gm.GroupId == groupId && gm.AppUserId.Equals(appUserId)))
            {
                return new List<GroupMessage> { };
            }
            List<GroupMessage> groupMessages = await _context.GroupMessages.Where(gm => gm.GroupId == groupId)
                .OrderBy(gm => gm.DateTime).ToListAsync();
            return groupMessages;
        }
    }
}
