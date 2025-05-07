using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TheApp.Application.AppointmentDTO.Commands.CreateAppointment;
using TheApp.Application.AppointmentDTO.Commands.DeleteAppointment;

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
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var id = await _mediator.Send(command);

            return Ok(new { message = "Created", id });
        }

        [Authorize]
        [HttpDelete]
        public async Task<IActionResult> DeleteById([FromBody] DeleteAppointmentCommand command)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            await _mediator.Send(new DeleteAppointmentCommand() { Id = command.Id });
            return Ok();
        }
    }
}