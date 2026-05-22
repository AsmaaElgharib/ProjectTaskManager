using MediatR;
using ProjectTaskManager.Application.Common.Models;
using ProjectTaskManager.Application.Features.Projects.Dtos;

namespace ProjectTaskManager.Application.Features.Projects.Commands.UpdateProject
{
    public record UpdateProjectCommand(
    Guid Id,
    string Name,
    string? Description
) : IRequest<ApiResponse<ProjectDto>>;
}
