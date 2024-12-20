using Microsoft.AspNetCore.Identity;

namespace Chatting_System.Models
{
    public class AppUser : IdentityUser
    {
        List<Group> Groups { get; set; }
    }
}
