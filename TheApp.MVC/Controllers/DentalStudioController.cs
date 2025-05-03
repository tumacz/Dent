using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TheApp.Application.ApplicationUser;
using TheApp.Application.DataTransferObjects.Commands.CreateDentalStudio;
using TheApp.Application.DataTransferObjects.Commands.EditDentalStudio;
using TheApp.Application.DataTransferObjects.Queries.GetAllDentaStudiosQuery;
using TheApp.Application.DataTransferObjects.Queries.GetDentalStudioByEncodedName;

namespace TheApp.MVC.Controllers
{
    [ApiController]
    [Route("api/dental-studio")]
    public class DentalStudioController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IUserContext _userContext;//temp
        private readonly IMapper _mapper;

        public DentalStudioController(IMediator mediator, IUserContext userContext, IMapper mapper)
        {
            _mediator = mediator;
            _userContext = userContext;
            _mapper = mapper;
        }
        #region temp
        [HttpGet("ping")]
        public IActionResult Ping()
        {
            return Ok("pong");
        }

        [HttpGet("me")]//temp
        public IActionResult GetCurrentUser()
        {
            var user = _userContext.GetCurrentUser();
            if (user == null)
                return Unauthorized("User is not authenticated");

            return Ok(new
            {
                user.Id,
                user.Email,
                Roles = user.Roles.ToList()
            });
        }
        #endregion

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var data = await _mediator.Send(new GetAllDentalStudiosQuery());
            return Ok(data);
        }

        [HttpGet("{encodedName}")]
        public async Task<IActionResult> Details(string encodedName)
        {
            var data = await _mediator.Send(new GetDentalStudioByEncodedNameQuery(encodedName));
            return Ok(data);
        }

        [HttpPost("create")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Create([FromBody] CreateDentalStudioCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            // TODO(macio): separate DTO for read and push
            var id = await _mediator.Send(command);

            return Ok(new { message = $"Created Dental Studio: {id}" });
        }

        [Authorize]
        [HttpPut("{encodedName}")]
        public async Task<IActionResult> Edit(string encodedName, [FromBody] EditDentalStudioCommand command)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            command.EncodedName = encodedName;
            await _mediator.Send(command);
            return NoContent();
        }
    }
}