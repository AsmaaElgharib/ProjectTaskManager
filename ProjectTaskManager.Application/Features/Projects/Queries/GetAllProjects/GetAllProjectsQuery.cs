using MediatR;
using ProjectTaskManager.Application.Common.Models;
using ProjectTaskManager.Application.Features.Projects.Dtos;

namespace ProjectTaskManager.Application.Features.Projects.Queries.GetAllProjects
{
    public record GetAllProjectsQuery
    : IRequest<ApiResponse<IEnumerable<ProjectDto>>>;
}
