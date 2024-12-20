using Chatting_System.Models;
using System.ComponentModel.DataAnnotations;

namespace Chatting_System.Dtos.GroupMessage
{
    public class CreateGroupMessageRequestDto
    {
        [Required]
        public int GroupId { get; set; }
        [Required]
        [MinLength(1)]
        public string Content { get; set; }
    }
}
