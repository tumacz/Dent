using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using TheApp.Application.ApplicationUser.UserDTO.Commands;
using TheApp.Application.ApplicationUser.UserDTO.Queries;

namespace TheApp.MVC.Controllers
{
    public class UserManagementSystem : Controller
    {
        private readonly IMediator _mediator;
        public UserManagementSystem(IMediator mediator) 
        {
            _mediator = mediator;
        }
        public async Task<IActionResult> Index()
        {
            var users = await _mediator.Send(new GetAllUsersQuery());
            return View(users);
        }

        [HttpPost]
        [Route("UserManagementSystem/UserEdit")]
        public async Task<IActionResult> EditUserRoles(EditUserCommand command)
        {
            await _mediator.Send(command);
            return RedirectToAction(nameof(Index));
        }
    }
}
