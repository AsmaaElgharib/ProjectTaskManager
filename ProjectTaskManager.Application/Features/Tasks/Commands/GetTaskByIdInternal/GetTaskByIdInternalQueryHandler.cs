using MediatR;
using ProjectTaskManager.Application.Common.Exceptions;
using ProjectTaskManager.Application.Common.Interfaces;
using ProjectTaskManager.Domain.Entities;
using ProjectTaskManager.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTaskManager.Application.Features.Tasks.Commands.GetTaskByIdInternal
{
    public class GetTaskByIdInternalQueryHandler
    : IRequestHandler<
        GetTaskByIdInternalQuery,
        TaskDto>
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly ICurrentUserService _currentUserService;

        public GetTaskByIdInternalQueryHandler(
            ITaskRepository taskRepository,
            IProjectRepository projectRepository,
            ICurrentUserService currentUserService)
        {
            _taskRepository = taskRepository;
            _projectRepository = projectRepository;
            _currentUserService = currentUserService;
        }

        public async Task<TaskDto> Handle(
            GetTaskByIdInternalQuery request,
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
            {
                throw new UnauthorizedException(
                    "You do not own this task's project.");
            }

            return new TaskDto(task.Id, 
                task.Title,
                task.Description, 
                task.Status.ToString(), 
                task.Priority.ToString(), 
                task.DueDate, 
                task.ProjectId,
                task.CreatedAt,
                task.UpdatedAt);
        }
    }
}
