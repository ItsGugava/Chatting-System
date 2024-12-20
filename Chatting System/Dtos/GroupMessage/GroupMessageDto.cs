namespace Chatting_System.Dtos.GroupMessage
{
    public class GroupMessageDto
    {
        public int Id { get; set; }
        public string SenderId { get; set; }
        public int GroupId { get; set; }
        public string Content { get; set; }
        public DateTime DateTime { get; set; } 
    }
}
