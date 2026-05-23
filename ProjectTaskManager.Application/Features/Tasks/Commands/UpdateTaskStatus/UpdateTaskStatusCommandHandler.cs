using MediatR;
using ProjectTaskManager.Application.Common.Exceptions;
using ProjectTaskManager.Application.Common.Interfaces;
using ProjectTaskManager.Application.Common.Models;
using ProjectTaskManager.Domain.Entities;
using ProjectTaskManager.Domain.Interfaces;

namespace ProjectTaskManager.Application.Features.Tasks.Commands.UpdateTaskStatus
{
    public class UpdateTaskStatusCommandHandler
: IRequestHandler<UpdateTaskStatusCommand, ApiResponse<TaskDto>>
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly ICurrentUserService _currentUserService;

        public UpdateTaskStatusCommandHandler(
            ITaskRepository taskRepository,
            IProjectRepository projectRepository,
            ICurrentUserService currentUserService)
        {
            _taskRepository = taskRepository;
            _projectRepository = projectRepository;
            _currentUserService = currentUserService;
        }

        public async Task<ApiResponse<TaskDto>> Handle(
            UpdateTaskStatusCommand request,
            CancellationToken cancellationToken)
        {
            var task =
                await _taskRepository.GetByIdAsync(
                    request.Id,
                    cancellationToken)
                ?? throw new NotFoundException(
                    nameof(ProjectTask),
                    request.Id);

            var exists =
                await _projectRepository.ExistsAsync(
                    task.ProjectId,
                    _currentUserService.UserId,
                    cancellationToken);

            if (!exists)
                throw new UnauthorizedException(
                    "You do not own this task.");

            task.Status = (Domain.Enums.TaskStatus)request.Status;
            task.UpdatedAt = DateTime.UtcNow;

            await _taskRepository.UpdateAsync(
                task, cancellationToken);

            return ApiResponse<TaskDto>.SuccessResult(
                new TaskDto(
                    task.Id,
                    task.Title,
                    task.Description,
                    task.Status.ToString(),
                    task.Priority.ToString(),
                    task.DueDate,
                    task.ProjectId,
                    task.CreatedAt,
                    task.UpdatedAt),
                "Status updated successfully.");
        }
    }
}
