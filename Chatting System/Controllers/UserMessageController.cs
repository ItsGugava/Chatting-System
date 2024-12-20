using Chatting_System.Dtos.UserMessage;
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
    public class UserMessageController : ControllerBase
    {
        private readonly IUserMessageRepository _userMessageRepo;

        public UserMessageController(IUserMessageRepository userMessageRepo)
        {
            _userMessageRepo = userMessageRepo;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] CreateUserMessageRequestDto createUserMessageDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            string? appUserId = User.GetId();
            UserMessage userMessage = createUserMessageDto.FromCreateDtoToUserMessage(appUserId);
            await _userMessageRepo.CreateAsync(userMessage);
            UserMessageDto userMessageDto = userMessage.FromUserMessageToUserMessageDto();
            return Ok(userMessageDto);
        }

        [HttpPut]
        [Authorize]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateUserMessageRequestDto updateUserMessageDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            string? appUserId = User.GetId();
            UserMessage? userMessage = await _userMessageRepo.UpdateAsync(id,  updateUserMessageDto, appUserId);
            if(userMessage == null)
            {
                return NotFound();
            }
            return Ok(userMessage.FromUserMessageToUserMessageDto());
        }

        [HttpDelete]
        [Authorize]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            string? appUserId = User.GetId();
            UserMessage? userMessage = await _userMessageRepo.DeleteAsync(id, appUserId);
            if( userMessage == null)
            {
                return NotFound();
            }
            return Ok("Message is deleted");
        }

        [HttpGet]
        [Authorize]
        [Route("{userId}")]
        public async Task<IActionResult> GetUserMessages([FromRoute] string userId)
        {
            string? authorizedUserId = User.GetId();
            List<UserMessage> userMessages = await _userMessageRepo.GetUserMessagesAsync(authorizedUserId, userId);
            List<UserMessageDto> userMessageDtos = userMessages.Select(u => u.FromUserMessageToUserMessageDto()).ToList();
            return Ok(userMessageDtos);
        }
    }
}
