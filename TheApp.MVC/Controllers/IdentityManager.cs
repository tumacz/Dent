using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TheApp.Application.UsersDTO.Commands.EditUser;
using TheApp.Application.UsersDTO.Queries.GetAllUsers;
using TheApp.Application.UsersDTO.Queries.GetUserByName;

namespace TheApp.MVC.Controllers
{
    public class IdentityManager : Controller
    {

        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public IdentityManager(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Index()
        {
            var users = await _mediator.Send(new GetAllUsersQuery());

            return View(users);
        }

        [Authorize(Roles = "Administrator")]
        [Route("IdentityManager/{userName}/Edit")]
        public async Task<IActionResult> Edit(string userName)
        {
            var dto = await _mediator.Send(new GetUserByNameQuery(userName));
            EditUserCommand model = _mapper.Map<EditUserCommand>(dto);

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        [Route("IdentityManager/{userName}/Edit")]
        public async Task<IActionResult> Edit(string userName, EditUserCommand command)
        {
            if (!ModelState.IsValid)
            {
                return View(command);
            }
            command.Name = userName;

            await _mediator.Send(command);

            return RedirectToAction(nameof(Index));
        }
    }
}
