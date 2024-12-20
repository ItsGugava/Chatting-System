using System.ComponentModel.DataAnnotations;

namespace Chatting_System.Dtos.Group
{
    public class CreateGroupRequestDto
    {
        [Required]
        [MinLength(1)]
        [MaxLength(25)]
        public string Name {  get; set; }
        [Required]
        [MinLength(2)]
        public List<string> AppUserIds { get; set; }
    }
}
