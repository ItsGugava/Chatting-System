namespace Chatting_System.Models
{
    public class GroupMessage
    {
        public int Id { get; set; }
        public string SenderId { get; set; }
        public int GroupId { get; set; }
        public string Content { get; set; }
        public DateTime DateTime { get; set; } = DateTime.Now;
        public AppUser Sender { get; set; }
        public Group Group { get; set; }
    }
}
