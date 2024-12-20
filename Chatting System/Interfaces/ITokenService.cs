using Chatting_System.Models;

namespace Chatting_System.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(AppUser appUser);
    }
}
