using Chatting_System.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Chatting_System.Dtos.UserMessage
{
    public class CreateUserMessageRequestDto
    {
        [Required]
        public string ReceiverId { get; set; }
        [Required]
        [MinLength(1)]
        public string Content { get; set; }
    }
}
