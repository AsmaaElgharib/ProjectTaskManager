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

namespace ProjectTaskManager.Application.Features.Tasks.Commands.UpdateTask
{
    public class UpdateTaskCommandHandler
: IRequestHandler<UpdateTaskCommand, ApiResponse<TaskDto>>
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly ICurrentUserService _currentUserService;

        public UpdateTaskCommandHandler(
            ITaskRepository taskRepository,
            IProjectRepository projectRepository,
            ICurrentUserService currentUserService)
        {
            _taskRepository = taskRepository;
            _projectRepository = projectRepository;
            _currentUserService = currentUserService;
        }

        public async Task<ApiResponse<TaskDto>> Handle(
            UpdateTaskCommand request,
            CancellationToken cancellationToken)
        {
            var task = await _taskRepository
                .GetByIdAsync(request.Id, cancellationToken)
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

            task.Title = request.Title;
            task.Description = request.Description;
            task.Status = request.Status;
            task.Priority = request.Priority;
            task.DueDate = request.DueDate;
            task.UpdatedAt = DateTime.UtcNow;

            await _taskRepository.UpdateAsync(
                task, cancellationToken);

            return ApiResponse<TaskDto>.SuccessResult(
             new TaskDto(task.Id, 
             task.Title,
             task.Description,
             task.Status.ToString(), 
             task.Priority.ToString(), 
             task.DueDate,
             task.ProjectId, 
             task.CreatedAt, 
             task.UpdatedAt),
             "Task updated successfully.");
        }
    }
}
