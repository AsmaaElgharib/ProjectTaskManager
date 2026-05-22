using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectTaskManager.Application.Features.Projects.Commands.CreateProject;
using ProjectTaskManager.Application.Features.Projects.Dtos;

namespace ProjectTaskManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProjectsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ProjectDto?>> GetById([FromRoute] int id)
        {
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProjectCommand command)
        {
            var result = await _mediator.Send(command);
            return CreatedAtAction(
               nameof(GetById),
               new { id = result.Data?.Id },
               result);
        }
    }
}
