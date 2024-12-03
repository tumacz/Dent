using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TheApp.Application.DataTransferObjects;
using TheApp.Application.DataTransferObjects.Commands.CreateDentalStudio;
using TheApp.Application.DataTransferObjects.Commands.EditDentalStudio;
using TheApp.Application.DataTransferObjects.Queries.GetAllDentaStudiosQuery;
using TheApp.Application.DataTransferObjects.Queries.GetDentalStudioByEncodedName;
using TheApp.Application.DentalStudioServiceDTO.Commands;
using TheApp.Application.DentalStudioServiceDTO.Queries;
using TheApp.Domain.Entities;
using TheApp.MVC.Extensions;
using TheApp.MVC.Models;

namespace TheApp.MVC.Controllers
{
    public class DentalStudioController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public DentalStudioController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [Route("DentalStudio/{encodedName}/Details")]
		public async Task<IActionResult> Details(string encodedName)
		{
            var details = await _mediator.Send(new GetDentalStudioByEncodedNameQuery(encodedName));
			return View(details);
		}

        public async Task<IActionResult> Index()
        {
            var dentalStudios = await _mediator.Send(new GetAllDentalStudiosQuery());
            return View(dentalStudios);
        }

        [Authorize(Roles = "Administrator")]
        public IActionResult Create()
        {
            if(!User.IsInRole("Administrator"))
            {
                RedirectToAction("NoAccess", "Home");
            }
            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(CreateDentalStudioCommand command)
        {
            if (!ModelState.IsValid)
            {
                return View(command);
            }

            await _mediator.Send(command);

            this.SetNotification("success", $"Created Dental Studio: {command.Name}");

            return RedirectToAction(nameof(Index));
        }

        [Route("DentalStudio/{encodedName}/Edit")]
        public async Task<IActionResult> Edit(string encodedName)
        {
            var dto = await _mediator.Send(new GetDentalStudioByEncodedNameQuery(encodedName));

            if(!dto.IsEditable)
            {
                return RedirectToAction("NoAccess", "Home");
            }

            EditDentalStudioCommand model = _mapper.Map<EditDentalStudioCommand>(dto);

            return View(model);
        }

        [HttpPost]
        [Route("DentalStudio/{encodedName}/Edit")]
        public async Task<IActionResult> Edit(string encodedName, EditDentalStudioCommand command)
        {
            if (!ModelState.IsValid)
            {
                return View(command);
            }


            await _mediator.Send(command);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [Authorize(Roles ="Moderator")]
        [Route("DentalStudio/DentalStudioService")]
        public async Task<IActionResult> CreateDentalStudioService(CreateDentalStudioServiceCommand command)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _mediator.Send(command);

            return Ok();
        }

        [HttpGet]
        [Route("DentalStudio/{encodedName}/DentalStudioService")]
        public async Task<IActionResult> GetDentalStudioServices(string encodedName)
        {
            var data = await _mediator.Send(new GetDentalStudioServiceForEncodedNameQuery(encodedName) { EncodedName = encodedName});
            return Ok(data);
        }
    }
}
