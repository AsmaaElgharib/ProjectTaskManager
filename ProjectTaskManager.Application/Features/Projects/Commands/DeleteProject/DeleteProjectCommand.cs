using MediatR;
using ProjectTaskManager.Application.Common.Models;

namespace ProjectTaskManager.Application.Features.Projects.Commands.DeleteProject
{

    public record DeleteProjectCommand(
        Guid Id
    ) : IRequest<ApiResponse>;
}
