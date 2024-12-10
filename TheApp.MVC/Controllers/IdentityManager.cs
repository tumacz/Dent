using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TheApp.Application.UsersDTO.Queries;

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
    }
}
