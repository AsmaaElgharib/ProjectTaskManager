using MediatR;
using ProjectTaskManager.Application.Common.Models;

namespace ProjectTaskManager.Application.Features.Tasks.Commands.DeleteTask
{
    public record DeleteTaskCommand(Guid Id) : IRequest<ApiResponse>;
}
