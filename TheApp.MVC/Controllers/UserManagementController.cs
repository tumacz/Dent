using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TheApp.Application.ApplicationUser.UserDTO.Commands;
using TheApp.Application.ApplicationUser.UserDTO.Queries;

namespace TheApp.MVC.Controllers
{
    [ApiController]
    [Route("api/user-management")]
    public class UserManagementController : ControllerBase
    {
        private readonly IMediator _mediator;
        public UserManagementController(IMediator mediator)
        {
            _mediator = mediator;
        }

        //[Authorize(Roles = "Administrator")]
        [HttpGet("all")]
        public async Task<IActionResult> Index()
        {
            var users = await _mediator.Send(new GetAllUsersQuery());
            return Ok(users);
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost("user-edit/{id}")]
        public async Task<IActionResult> EditUserRoles(string id,[FromBody] EditUserCommand command)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            command.Id = id;
            await _mediator.Send(command);
            return NoContent();
        }
    }
}