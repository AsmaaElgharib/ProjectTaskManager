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

namespace ProjectTaskManager.Application.Features.Tasks.Commands.DeleteTask
{
    public class DeleteTaskCommandHandler : IRequestHandler<DeleteTaskCommand, ApiResponse>
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly ICurrentUserService _currentUserService;

        public DeleteTaskCommandHandler(ITaskRepository taskRepository, IProjectRepository projectRepository, ICurrentUserService currentUserService)
        {
            _taskRepository = taskRepository;
            _projectRepository = projectRepository;
            _currentUserService = currentUserService;
        }

        public async Task<ApiResponse> Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
        {
            var task = await _taskRepository.GetByIdAsync(request.Id, cancellationToken)
                ?? throw new NotFoundException(nameof(ProjectTask), request.Id);

            var projectExists = await _projectRepository.ExistsAsync(task.ProjectId, _currentUserService.UserId, cancellationToken);
            if (!projectExists)
                throw new UnauthorizedException("You do not own this task's project.");

            await _taskRepository.DeleteAsync(task, cancellationToken);

            return ApiResponse.SuccessResult("Task deleted successfully.");
        }
    }
}
