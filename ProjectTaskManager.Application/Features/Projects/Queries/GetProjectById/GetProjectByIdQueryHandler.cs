using MediatR;
using ProjectTaskManager.Application.Common.Exceptions;
using ProjectTaskManager.Application.Common.Interfaces;
using ProjectTaskManager.Application.Common.Models;
using ProjectTaskManager.Application.Features.Projects.Dtos;
using ProjectTaskManager.Domain.Entities;
using ProjectTaskManager.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTaskManager.Application.Features.Projects.Queries.GetProjectById
{
    public class GetProjectByIdQueryHandler
    : IRequestHandler<
        GetProjectByIdQuery,
        ApiResponse<ProjectDetailDto>>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly ICurrentUserService _currentUserService;

        public GetProjectByIdQueryHandler(
            IProjectRepository projectRepository,
            ICurrentUserService currentUserService)
        {
            _projectRepository = projectRepository;
            _currentUserService = currentUserService;
        }

        public async Task<ApiResponse<ProjectDetailDto>> Handle(
            GetProjectByIdQuery request,
            CancellationToken cancellationToken)
        {
            var project =
                await _projectRepository
                    .GetByIdWithTasksAsync(
                        request.Id,
                        cancellationToken)
                ?? throw new NotFoundException(
                        nameof(Project),
                        request.Id);

            if (project.UserId != _currentUserService.UserId)
                throw new UnauthorizedException(
                    "You do not own this project.");

            var tasks = project.Tasks.Select(t =>
                new ProjectTaskSummaryDto(
                    t.Id,
                    t.Title,
                    t.Status.ToString(),
                    t.Priority.ToString(),
                    t.DueDate
                ));

            var dto = new ProjectDetailDto(
                project.Id,
                project.Name,
                project.Description,
                project.CreatedAt,
                project.UpdatedAt,
                tasks);

            return ApiResponse<ProjectDetailDto>
                .SuccessResult(dto);
        }
    }
}
