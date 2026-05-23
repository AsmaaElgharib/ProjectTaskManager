using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectTaskManager.Application.Common.Models;
using ProjectTaskManager.Application.Features.Projects.Commands.CreateProject;
using ProjectTaskManager.Application.Features.Projects.Commands.DeleteProject;
using ProjectTaskManager.Application.Features.Projects.Commands.UpdateProject;
using ProjectTaskManager.Application.Features.Projects.Dtos;
using ProjectTaskManager.Application.Features.Projects.Queries.GetAllProjects;
using ProjectTaskManager.Application.Features.Projects.Queries.GetProjectById;

namespace ProjectTaskManager.API.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProjectsController(IMediator mediator) => _mediator = mediator;


        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetAllProjectsQuery(), cancellationToken);
            return Ok(result);
        }

        ///Get a project by ID.
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetProjectByIdQuery(id), cancellationToken);
            return Ok(result);
        }

        /// Create a new project.
        [HttpPost] 
        public async Task<IActionResult> Create([FromBody] CreateProjectCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return CreatedAtAction(nameof(GetById), new { id = result.Data!.Id }, result);
        }

        ///Update an existing project.
        [HttpPut("{id:guid}")] 
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateProjectRequest request, CancellationToken cancellationToken)
        {
            var command = new UpdateProjectCommand(id, request.Name, request.Description);
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }

        ///Delete a project.
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new DeleteProjectCommand(id), cancellationToken);
            return Ok(result);
        }
    }

    public record UpdateProjectRequest(string Name, string? Description);
}
