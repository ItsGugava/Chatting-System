using System.ComponentModel.DataAnnotations;

namespace Chatting_System.Dtos.Account
{
    public class LoginDto
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
