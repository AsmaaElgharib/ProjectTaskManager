using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectTaskManager.Application.Common.Models;
using ProjectTaskManager.Application.Features.Tasks;
using ProjectTaskManager.Application.Features.Tasks.Commands.CreateTask;
using ProjectTaskManager.Application.Features.Tasks.Commands.DeleteTask;
using ProjectTaskManager.Application.Features.Tasks.Commands.GetTaskByIdInternal;
using ProjectTaskManager.Application.Features.Tasks.Commands.UpdateTask;
using ProjectTaskManager.Application.Features.Tasks.Queries.GetTasksByProject;
using ProjectTaskManager.Domain.Enums;
using TaskStatus = ProjectTaskManager.Domain.Enums.TaskStatus;

namespace ProjectTaskManager.API.Controllers;

[Route("api/v1")]
[ApiController]
[Produces("application/json")]
public class TasksController : ControllerBase
{
    private readonly IMediator _mediator;

    public TasksController(IMediator mediator) => _mediator = mediator;

    /// <summary>Get all tasks for a specific project.</summary>
    [HttpGet("projects/{projectId:guid}/tasks")]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<TaskDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByProject(Guid projectId, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetTasksByProjectQuery(projectId), cancellationToken);
        return Ok(result);
    }

    /// <summary>Create a task in a project.</summary>
    [HttpPost("projects/{projectId:guid}/tasks")]
    [ProducesResponseType(typeof(ApiResponse<TaskDto>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Create(Guid projectId, [FromBody] CreateTaskRequest request, CancellationToken cancellationToken)
    {
        var command = new CreateTaskCommand(request.Title, request.Description, request.Priority, request.DueDate, projectId);
        var result = await _mediator.Send(command, cancellationToken);
        return StatusCode(StatusCodes.Status201Created, result);
    }

    /// <summary>Update a task (full update).</summary>
    [HttpPut("tasks/{id:guid}")]
    [ProducesResponseType(typeof(ApiResponse<TaskDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateTaskRequest request, CancellationToken cancellationToken)
    {
        var command = new UpdateTaskCommand(id, request.Title, request.Description, request.Status, request.Priority, request.DueDate);
        var result = await _mediator.Send(command, cancellationToken);
        return Ok(result);
    }

    /// <summary>Update only the status of a task.</summary>
    [HttpPatch("tasks/{id:guid}/status")]
    [ProducesResponseType(typeof(ApiResponse<TaskDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateStatus(Guid id, [FromBody] UpdateTaskStatusRequest request, CancellationToken cancellationToken)
    {
        var task = await _mediator.Send(new GetTaskByIdInternalQuery(id), cancellationToken);
        var command = new UpdateTaskCommand(id, task.Title, task.Description, request.Status,
            Enum.Parse<TaskPriority>(task.Priority), task.DueDate);
        var result = await _mediator.Send(command, cancellationToken);
        return Ok(result);
    }

    /// <summary>Delete a task.</summary>
    [HttpDelete("tasks/{id:guid}")]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new DeleteTaskCommand(id), cancellationToken);
        return Ok(result);
    }
}

public record CreateTaskRequest(string Title, string? Description, TaskPriority Priority, DateTime? DueDate);
public record UpdateTaskRequest(string Title, string? Description, TaskStatus Status, TaskPriority Priority, DateTime? DueDate);
public record UpdateTaskStatusRequest(TaskStatus Status);
