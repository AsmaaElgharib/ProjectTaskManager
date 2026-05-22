using MediatR;
using ProjectTaskManager.Application.Common.Models;
using ProjectTaskManager.Application.Features.Projects.Dtos;

namespace ProjectTaskManager.Application.Features.Projects.Commands.CreateProject
{
    public record CreateProjectCommand : IRequest<ApiResponse<ProjectDto>>
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
    }
}
