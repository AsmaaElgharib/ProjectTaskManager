using MediatR;
using ProjectTaskManager.Application.Common.Models;

namespace ProjectTaskManager.Application.Features.Tasks.Commands.UpdateTaskStatus
{

    public record UpdateTaskStatusCommand(
        Guid Id,
        TaskStatus Status)
        : IRequest<ApiResponse<TaskDto>>;
}
