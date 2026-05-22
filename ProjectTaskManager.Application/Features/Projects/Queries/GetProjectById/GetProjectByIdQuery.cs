using MediatR;
using ProjectTaskManager.Application.Common.Models;
using ProjectTaskManager.Application.Features.Projects.Dtos;

namespace ProjectTaskManager.Application.Features.Projects.Queries.GetProjectById
{
    public record GetProjectByIdQuery(Guid Id)
    : IRequest<ApiResponse<ProjectDetailDto>>;
}
