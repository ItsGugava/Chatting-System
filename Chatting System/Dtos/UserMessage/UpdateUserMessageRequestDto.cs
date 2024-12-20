using System.ComponentModel.DataAnnotations;

namespace Chatting_System.Dtos.UserMessage
{
    public class UpdateUserMessageRequestDto
    {
        [Required]
        [MinLength(1)]
        public string Content { get; set; }
    }
}
