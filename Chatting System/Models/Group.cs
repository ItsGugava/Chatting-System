namespace Chatting_System.Models
{
    public class Group
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<GroupMember> GroupMembers { get; set; }
        public List<GroupMessage> GroupMessages { get; set; }
    }
}
