using System.ComponentModel.DataAnnotations;

namespace Chatting_System.Dtos.Group
{
    public class UpdateGroupRequestDto
    {
        [Required]
        [MinLength(1)]
        [MaxLength(25)]
        public string Name { get; set; }
    }
}
