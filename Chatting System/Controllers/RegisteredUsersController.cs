using Chatting_System.Dtos.RegisteredUsers;
using Chatting_System.Extensions;
using Chatting_System.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Chatting_System.Controllers
{
    [Route("[Controller]")]
    [ApiController]
    public class RegisteredUsersController : ControllerBase
    {
        private readonly IRegisteredUsersRepository _registeredUsersRepo;

        public RegisteredUsersController(IRegisteredUsersRepository registeredUsersRepo)
        {
            _registeredUsersRepo = registeredUsersRepo;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllRegisteredUsers()
        {
            string appUserId = User.GetId();
            List<RegisteredUsersDto> registeredUsersDtos = await _registeredUsersRepo.GetAllAsync(appUserId);
            return Ok(registeredUsersDtos);
        }
    }
}
