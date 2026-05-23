using MediatR;
using ProjectTaskManager.Application.Common.Models;
using ProjectTaskManager.Domain.Enums;

namespace ProjectTaskManager.Application.Features.Tasks.Commands.UpdateTask
{

    public record UpdateTaskCommand(
        Guid Id,
        string Title,
        string? Description,
        Domain.Enums.TaskStatus Status,
        TaskPriority Priority,
        DateTime? DueDate
    ) : IRequest<ApiResponse<TaskDto>>;
}
