using System.ComponentModel.DataAnnotations;

namespace Chatting_System.Dtos.Account
{
    public class RegisterDto
    {
        [Required]
        [MinLength(5)]
        [MaxLength(50)]
        public string UserName { get; set; }
        [Required]
        [MinLength(5)]
        [MaxLength(50)]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
