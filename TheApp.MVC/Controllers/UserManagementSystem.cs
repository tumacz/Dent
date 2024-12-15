using MediatR;
using Microsoft.AspNetCore.Mvc;
using TheApp.Application.ApplicationUser.UserDTO.Queries;
using TheApp.Application.DataTransferObjects.Queries.GetAllDentaStudiosQuery;

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
    }
}
