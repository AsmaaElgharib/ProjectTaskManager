using MediatR;
using ProjectTaskManager.Application.Common.Exceptions;
using ProjectTaskManager.Application.Common.Interfaces;
using ProjectTaskManager.Application.Common.Models;
using ProjectTaskManager.Application.Features.Projects.Dtos;
using ProjectTaskManager.Domain.Entities;
using ProjectTaskManager.Domain.Interfaces;


namespace ProjectTaskManager.Application.Features.Projects.Commands.UpdateProject
{
    public class UpdateProjectCommandHandler : IRequestHandler<UpdateProjectCommand,
        ApiResponse<ProjectDto>>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly ICurrentUserService _currentUserService;

        public UpdateProjectCommandHandler(
            IProjectRepository projectRepository,
            ICurrentUserService currentUserService)
        {
            _projectRepository = projectRepository;
            _currentUserService = currentUserService;
        }

        public async Task<ApiResponse<ProjectDto>> Handle(
        UpdateProjectCommand request,
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

            project.Name = request.Name;
            project.Description = request.Description;
            project.UpdatedAt = DateTime.UtcNow;

            await _projectRepository
                .UpdateAsync(project, cancellationToken);

            var dto = new ProjectDto(
                project.Id,
                project.Name,
                project.Description,
                project.CreatedAt,
                project.UpdatedAt,
                project.Tasks.Count());

            return ApiResponse<ProjectDto>
                .SuccessResult(dto,
                "Project updated successfully.");
        }
    }
}
