using Chatting_System.Dtos.Group;
using Chatting_System.Models;

namespace Chatting_System.Mappers
{
    public static class GroupMapper
    {
        public static GroupDto FromGroupToGroupDto(this Group group)
        {
            return new GroupDto
            {
                Id = group.Id,
                Name = group.Name,
            };
        }
    }
}
