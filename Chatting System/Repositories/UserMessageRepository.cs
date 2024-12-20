using Chatting_System.Data;
using Chatting_System.Dtos.UserMessage;
using Chatting_System.Interfaces;
using Chatting_System.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Chatting_System.Repositories
{
    public class UserMessageRepository : IUserMessageRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public UserMessageRepository(ApplicationDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task CreateAsync(UserMessage userMessage)
        {
             await _context.UserMessages.AddAsync(userMessage);
             await _context.SaveChangesAsync();
        }

        public async Task<UserMessage?> DeleteAsync(int id, string appUserId)
        {
            UserMessage? userMessage = await _context.UserMessages.FirstOrDefaultAsync(m => m.Id.Equals(id));
            if(userMessage == null)
            {
                return null;
            }
            if(!userMessage.SenderId.Equals(appUserId))
            {
                return null;
            }
            _context.Remove(userMessage);
            await _context.SaveChangesAsync();
            return userMessage;
        }

        public async Task<List<UserMessage>> GetUserMessagesAsync(string authorizedUserId, string otherUserId)
        {
            List<UserMessage> userMessages = await _context.UserMessages
                .Where(m => (m.SenderId.Equals(authorizedUserId) && m.ReceiverId.Equals(otherUserId)) || (m.SenderId.Equals(otherUserId) && m.ReceiverId.Equals(authorizedUserId)))
                .OrderBy(m => m.DateTime)
                .ToListAsync();
            return userMessages;
        }

        public async Task<UserMessage?> UpdateAsync(int id, UpdateUserMessageRequestDto updateUserMessageDto, string appUserId)
        {
            UserMessage? userMessage = await _context.UserMessages.FirstOrDefaultAsync(m => m.Id.Equals(id));
            if (userMessage == null)
            {
                return null;
            }
            if(!userMessage.SenderId.Equals(appUserId))
            {
                return null;
            }
            userMessage.Content = updateUserMessageDto.Content;
            userMessage.DateTime = DateTime.Now;
            await _context.SaveChangesAsync();
            return userMessage;
        }
    }
}
