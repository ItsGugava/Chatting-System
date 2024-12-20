using System.ComponentModel.DataAnnotations.Schema;

namespace Chatting_System.Models
{
    public class UserMessage
    {
        public int Id { get; set; }
        public string SenderId { get; set; }
        public string ReceiverId { get; set; }
        public string Content { get; set; }
        public DateTime DateTime { get; set; } = DateTime.Now;
        public AppUser Sender { get; set; }
        public AppUser Receiver { get; set; } 
    }
}
