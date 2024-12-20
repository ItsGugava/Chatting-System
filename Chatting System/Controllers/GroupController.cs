using Chatting_System.Dtos.Group;
using Chatting_System.Extensions;
using Chatting_System.Interfaces;
using Chatting_System.Mappers;
using Chatting_System.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Chatting_System.Controllers
{
    [Route("[Controller]")]
    [ApiController]
    public class GroupController : ControllerBase
    {
        private readonly IGroupRepository _groupRepo;
        public GroupController(IGroupRepository groupRepo)
        {
            _groupRepo = groupRepo;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] CreateGroupRequestDto groupDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            string? appUserId = User.GetId();
            groupDto.AppUserIds.Add(appUserId);
            Group? group = await _groupRepo.CreateAsync(groupDto);
            if (group == null)
            {
                return BadRequest();
            }
            return Ok(group.FromGroupToGroupDto());
        }

        [HttpPut]
        [Authorize]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateGroupRequestDto groupDto)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            string? appUserId = User.GetId();
            Group? group = await _groupRepo.UpdateAsync(id, groupDto, appUserId);
            if(group == null)
            {
                return BadRequest("Group not found or user doesn't have permission");
            }
            return Ok(group.FromGroupToGroupDto());
        }

        [HttpDelete]
        [Authorize]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            string? appUserId = User.GetId();
            Group? group = await _groupRepo.DeleteAsync(id, appUserId);
            if (group == null)
            {
                return BadRequest("Group not found or user doesn't have permission");
            }
            return Ok("Group is Deleted");
        }

        [HttpGet("MyGroups")]
        [Authorize]
        public IActionResult GetMyGroups()
        {
            string appUserId = User.GetId();
            List<Group> groups = _groupRepo.GetMyGroups(appUserId);
            List<GroupDto> groupDtos = groups.Select(g => g.FromGroupToGroupDto()).ToList();
            return Ok(groupDtos);
        }

        [HttpDelete]
        [Authorize]
        [Route("LeaveGroup/{id}")]
        public async Task<IActionResult> LeaveGroup([FromRoute] int id)
        {
            string appUserId = User.GetId();
            GroupMember? groupMember = await _groupRepo.LeaveGroupAsync(id, appUserId);
            if(groupMember == null)
            {
                return BadRequest("Group not found or user is not in group");
            }
            return Ok("Group was left");
        }

        [HttpDelete]
        [Authorize]
        [Route("RemoveUser/{id}")]
        public async Task<IActionResult> RemoveUser([FromRoute] int id, [FromBody] RemoveUserRequestDto removeUserRequestDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            string? appUserId = User.GetId();
            GroupMember? groupMember = await _groupRepo.RemoveUserAsync(id, removeUserRequestDto, appUserId);
            if(groupMember == null)
            {
                return BadRequest("User not found in group or no permission");
            }
            return Ok("User is Removed");
        }

        [HttpPost]
        [Authorize]
        [Route("AddUser/{id}")]
        public async Task<IActionResult> AddUser([FromRoute] int id, [FromBody] AddUserRequestDto addUserRequestDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            string? appUserId = User.GetId();
            GroupMember? groupMember = await _groupRepo.AddUserAsync(id, addUserRequestDto, appUserId);
            if(groupMember == null)
            {
                return BadRequest("User not found or no permission");
            }
            return Ok("User is Added");
        }

        [HttpGet]
        [Authorize]
        [Route("GroupMembers/{id}")]
        public async Task<IActionResult> GetGroupMembers([FromRoute] int id)
        {
            string? appUserId = User.GetId();
            List<GroupMemberDto> groupMemberDtos = await _groupRepo.GetGroupMembersAsync(id, appUserId);
            return Ok(groupMemberDtos);
        }
    }
}
