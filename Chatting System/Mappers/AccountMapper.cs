using Chatting_System.Dtos.Account;
using Chatting_System.Models;

namespace Chatting_System.Mappers
{
    public static class AccountMapper
    {
        public static AppUser FromRegisterDtoToAppUser(this RegisterDto registerDto)
        {
            return new AppUser
            {
                UserName = registerDto.UserName,
                Email = registerDto.Email
            };
        }
        public static UserDto FromAppUserToUserDto(this AppUser appUser, string token)
        {
            return new UserDto
            {
                UserName = appUser.UserName,
                Token = token
            };
        }
    }
}
