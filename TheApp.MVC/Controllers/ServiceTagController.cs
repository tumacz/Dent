using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TheApp.Application.ApplicationUser;
using TheApp.Application.ServiceTag_CQRS.Commands.CreateServiceTag;
using TheApp.Application.ServiceTag_CQRS.Queries;
using TheApp.Application.ServiceTag_CQRS.Queries.GetAllServiceTags;
using TheApp.Application.ServiceTagDTO.Commands;

namespace TheApp.MVC.Controllers
{
	[ApiController]
	[Route("api/service-tags")]
	public class ServiceTagController : ControllerBase
	{
		private readonly IMediator _mediator;
		private readonly IMapper _mapper;
		private readonly IUserContext _userContext;

		public ServiceTagController(IMediator mediator, IMapper mapper, IUserContext userContext)
		{
			_mediator = mediator;
			_mapper = mapper;
			_userContext = userContext;
		}

		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			var tags = await _mediator.Send(new GetAllServiceTagsQuery());
			return Ok(tags);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetById(int id)
		{
			var tag = await _mediator.Send(new GetServiceTagByIdQuery(id));
			return tag == null ? NotFound() : Ok(tag);
		}

		[Authorize(Roles = "Administrator")]
		[HttpPost]
		public async Task<IActionResult> Create([FromBody] CreateServiceTagCommand command)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var id = await _mediator.Send(command);
			return Ok(new { message = "Created", id });
		}

		[Authorize(Roles = "Administrator")]
		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			await _mediator.Send(new DeleteServiceTagCommand { Id = id });
			return Ok();
		}
	}
}