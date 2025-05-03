using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TheApp.Application.DentalStudioServiceDTO.Commands;
using TheApp.Application.DentalStudioServiceDTO.Queries;

namespace TheApp.MVC.Controllers
{
    [ApiController]
    [Route("api/dental-service")]
    public class DentalServicesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public DentalServicesController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(CreateDentalStudioServiceCommand command)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var id = await _mediator.Send(command);

            return Ok(new {message = "Created", id});
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var data = await _mediator.Send(new GetDentalStudioServiceByIdQuery(id));
            return data == null ? NotFound() : Ok(data);
        }

        [HttpGet("by-studio/{encodedName}")]
        public async Task<IActionResult> GetByStudio(string encodedName)
        {
            var data = await _mediator.Send(new GetDentalStudioServiceForEncodedNameQuery(encodedName) { EncodedName = encodedName });
            return Ok(data);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteById(int id)
        {
            await _mediator.Send(new DeleteDentalStudioServiceByIdCommand() { Id = id });
            return Ok();
        }
    }
}
