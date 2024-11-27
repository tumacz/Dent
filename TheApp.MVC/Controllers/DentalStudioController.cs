using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TheApp.Application.DataTransferObjects;
using TheApp.Application.DataTransferObjects.Commands.CreateDentalStudio;
using TheApp.Application.DataTransferObjects.Commands.EditDentalStudio;
using TheApp.Application.DataTransferObjects.Queries.GetAllDentaStudiosQuery;
using TheApp.Application.DataTransferObjects.Queries.GetDentalStudioByEncodedName;
using TheApp.Domain.Entities;

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

        [Authorize]
        public IActionResult Create()
        {
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
    }
}
