using Chatting_System.Data;
using Chatting_System.Dtos.RegisteredUsers;
using Chatting_System.Interfaces;
using Chatting_System.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Chatting_System.Repositories
{
    public class RegisteredUsersRepository : IRegisteredUsersRepository
    {
        private readonly UserManager<AppUser> _userManager;
        
        public RegisteredUsersRepository(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<List<RegisteredUsersDto>> GetAllAsync(string appUserId)
        {
            List<AppUser> appUsers = await _userManager.Users.Where(u => !u.Id.Equals(appUserId)).ToListAsync();
            List<RegisteredUsersDto> registeredUsersDtos = appUsers.Select(u => new RegisteredUsersDto { Id = u.Id, UserName = u.UserName}).ToList();
            return registeredUsersDtos;
        }
    }
}
