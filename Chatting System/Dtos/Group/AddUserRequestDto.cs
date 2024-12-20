using System.ComponentModel.DataAnnotations;

namespace Chatting_System.Dtos.Group
{
    public class AddUserRequestDto
    {
        [Required]
        public string AppUserId { get; set; }
    }
}
