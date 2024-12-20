using System.ComponentModel.DataAnnotations;

namespace Chatting_System.Dtos.Group
{
    public class RemoveUserRequestDto
    {
        [Required]
        public string AppUserId { get; set; }
    }
}
