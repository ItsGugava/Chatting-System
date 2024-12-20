using Chatting_System.Dtos.GroupMessage;
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
    public class GroupMessageController : ControllerBase
    {
        private readonly IGroupMessageRepository _groupMessageRepo;

        public GroupMessageController(IGroupMessageRepository groupMessageRepo)
        {
            _groupMessageRepo = groupMessageRepo;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] CreateGroupMessageRequestDto groupMessageDto)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            string? appUserId = User.GetId();
            GroupMessage groupMessage = groupMessageDto.FromCreateDtoToGroupMessage(appUserId);
            GroupMessage? resultGroupMessage = await _groupMessageRepo.CreateAsync(groupMessage);
            if (resultGroupMessage == null)
            {
                return BadRequest("User is not in group");
            }
            return Ok(resultGroupMessage.FromGroupMessageToGroupMessageDto());
        }

        [HttpGet]
        [Authorize]
        [Route("{groupId}")]
        public async Task<IActionResult> GetAllGroupMessages([FromRoute] int groupId)
        {
            string? appUserId = User.GetId();
            List<GroupMessage> groupMessages = await _groupMessageRepo.GetAllAsync(groupId, appUserId);
            List<GroupMessageDto> groupMessagesDto= groupMessages.Select(gm => gm.FromGroupMessageToGroupMessageDto()).ToList();
            return Ok(groupMessagesDto);
        }

        [HttpDelete]
        [Authorize]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            string? appUserId = User.GetId();
            GroupMessage? groupMessage = await _groupMessageRepo.DeleteAsync(id, appUserId);
            if (groupMessage == null)
            {
                return NotFound();
            }
            return Ok("Message is deleted");
        }
    }
}
