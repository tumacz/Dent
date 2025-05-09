using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TheApp.Application.AppointmentDTO.Commands.CreateAppointment;
using TheApp.Application.AppointmentDTO.Commands.DeleteAppointment;
using TheApp.Application.AppointmentDTO.Queries.GetAppointmentsForCurrentUser;
using TheApp.Application.AppointmentDTO.Queries.GetAppointmentsForStudio;

namespace TheApp.MVC.Controllers
{
	[ApiController]
	[Route("api/appointments")]
	public class AppointmentController : ControllerBase
	{
		private readonly IMediator _mediator;

		public AppointmentController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[Authorize]
		[HttpPost]
		public async Task<IActionResult> Create([FromBody] CreateAppointmentCommand command)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var id = await _mediator.Send(command);
			return Ok(new { message = "Created", id });
		}

		[Authorize]
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteById(int id)
		{
			await _mediator.Send(new DeleteAppointmentCommand { Id = id });
			return Ok();
		}

		[Authorize]
		[HttpGet("{id}")]
		public async Task<IActionResult> GetById(int id)
		{
			var result = await _mediator.Send(new GetAppointmentByIdQuery(id));
			return result == null ? NotFound() : Ok(result);
		}

		[Authorize]
		[HttpGet("mine")]
		public async Task<IActionResult> GetMine()
		{
			var result = await _mediator.Send(new GetAppointmentsForCurrentUserQuery());
			return Ok(result);
		}

		[Authorize]
		[HttpGet("by-studio/{encodedName}")]
		public async Task<IActionResult> GetByStudio(string encodedName)
		{
			var result = await _mediator.Send(new GetAppointmentsForStudioQuery { StudioEncodedName = encodedName });
			return Ok(result);
		}
	}
}
