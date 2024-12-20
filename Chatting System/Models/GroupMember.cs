namespace Chatting_System.Models
{
    public class GroupMember
    {
        public int GroupId { get; set; }
        public string AppUserId { get; set; }
        public Group Group { get; set; }
        public AppUser AppUser { get; set; }
    }
}
