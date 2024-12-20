using Chatting_System.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Chatting_System.Dtos.UserMessage
{
    public class UserMessageDto
    {
        public int Id { get; set; }
        public string SenderId { get; set; }
        public string ReceiverId { get; set; }
        public string Content { get; set; }
        public DateTime DateTime { get; set; } 
    }
}
