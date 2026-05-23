using MediatR;
using ProjectTaskManager.Application.Common.Exceptions;
using ProjectTaskManager.Application.Common.Interfaces;
using ProjectTaskManager.Application.Common.Models;
using ProjectTaskManager.Domain.Entities;
using ProjectTaskManager.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTaskManager.Application.Features.Tasks.Queries.GetTasksByProject
{
    public class GetTasksByProjectQueryHandler
    : IRequestHandler<
        GetTasksByProjectQuery,
        ApiResponse<IEnumerable<TaskDto>>>
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly ICurrentUserService _currentUserService;

        public GetTasksByProjectQueryHandler(
            ITaskRepository taskRepository,
            IProjectRepository projectRepository,
            ICurrentUserService currentUserService)
        {
            _taskRepository = taskRepository;
            _projectRepository = projectRepository;
            _currentUserService = currentUserService;
        }

        public async Task<ApiResponse<IEnumerable<TaskDto>>> Handle(
            GetTasksByProjectQuery request,
            CancellationToken cancellationToken)
        {
            var projectExists =
                await _projectRepository.ExistsAsync(
                    request.ProjectId,
                    _currentUserService.UserId,
                    cancellationToken);

            if (!projectExists)
            {
                throw new NotFoundException(
                    nameof(Project),
                    request.ProjectId);
            }

            var tasks =
                await _taskRepository.GetByProjectIdAsync(
                    request.ProjectId,
                    cancellationToken);

            var dtos = tasks.Select(t =>
                new TaskDto(
                    t.Id,
                    t.Title,
                    t.Description,
                    t.Status.ToString(),
                    t.Priority.ToString(),
                    t.DueDate,
                    t.ProjectId,
                    t.CreatedAt,
                    t.UpdatedAt
                ));

            return ApiResponse<IEnumerable<TaskDto>>
                .SuccessResult(dtos);
        }
    }
}
