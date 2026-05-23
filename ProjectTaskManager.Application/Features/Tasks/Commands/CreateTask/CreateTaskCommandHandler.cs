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

namespace ProjectTaskManager.Application.Features.Tasks.Commands.CreateTask
{
    public class CreateTaskCommandHandler
    : IRequestHandler<CreateTaskCommand, ApiResponse<TaskDto>>
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly ICurrentUserService _currentUserService;

        public CreateTaskCommandHandler(
            ITaskRepository taskRepository,
            IProjectRepository projectRepository,
            ICurrentUserService currentUserService)
        {
            _taskRepository = taskRepository;
            _projectRepository = projectRepository;
            _currentUserService = currentUserService;
        }

        public async Task<ApiResponse<TaskDto>> Handle(
            CreateTaskCommand request,
            CancellationToken cancellationToken)
        {
            var exists = await _projectRepository
                .ExistsAsync(
                    request.ProjectId,
                    _currentUserService.UserId,
                    cancellationToken);

            if (!exists)
                throw new NotFoundException(
                    nameof(Project),
                    request.ProjectId);

            var task = new ProjectTask
            {
                Title = request.Title,
                Description = request.Description,
                Priority = request.Priority,
                DueDate = request.DueDate,
                ProjectId = request.ProjectId
            };

            var created = await _taskRepository
                .AddAsync(task, cancellationToken);

            return ApiResponse<TaskDto>
                .SuccessResult(
                    MapToDto(created),
                    "Task created successfully.");
        }

        private static TaskDto MapToDto(ProjectTask t)
            => new(
                t.Id,
                t.Title,
                t.Description,
                t.Status.ToString(),
                t.Priority.ToString(),
                t.DueDate,
                t.ProjectId,
                t.CreatedAt,
                t.UpdatedAt);
    }
}
