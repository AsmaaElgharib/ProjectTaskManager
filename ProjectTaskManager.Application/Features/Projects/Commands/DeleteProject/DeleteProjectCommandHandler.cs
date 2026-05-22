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

namespace ProjectTaskManager.Application.Features.Projects.Commands.DeleteProject
{
    public class DeleteProjectCommandHandler
    : IRequestHandler<DeleteProjectCommand,
        ApiResponse>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly ICurrentUserService _currentUserService;

        public DeleteProjectCommandHandler(
            IProjectRepository projectRepository,
            ICurrentUserService currentUserService)
        {
            _projectRepository = projectRepository;
            _currentUserService = currentUserService;
        }

        public async Task<ApiResponse> Handle(
            DeleteProjectCommand request,
            CancellationToken cancellationToken)
        {
            var project =
                await _projectRepository.GetByIdAsync(
                    request.Id,
                    cancellationToken)
                ?? throw new NotFoundException(
                    nameof(Project),
                    request.Id);

            if (project.UserId != _currentUserService.UserId)
                throw new UnauthorizedException(
                    "You do not own this project.");

            await _projectRepository
                .DeleteAsync(project,
                    cancellationToken);

            return ApiResponse.SuccessResult(
                "Project deleted successfully.");
        }
    }
}
