using MediatR;
using ProjectTaskManager.Application.Common.Interfaces;
using ProjectTaskManager.Application.Common.Models;
using ProjectTaskManager.Application.Features.Projects.Dtos;
using ProjectTaskManager.Domain.Interfaces;

namespace ProjectTaskManager.Application.Features.Projects.Queries.GetAllProjects
{
    public class GetAllProjectsQueryHandler
    : IRequestHandler<
        GetAllProjectsQuery,
        ApiResponse<IEnumerable<ProjectDto>>>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly ICurrentUserService _currentUserService;

        public GetAllProjectsQueryHandler(
            IProjectRepository projectRepository,
            ICurrentUserService currentUserService)
        {
            _projectRepository = projectRepository;
            _currentUserService = currentUserService;
        }

        public async Task<ApiResponse<IEnumerable<ProjectDto>>> Handle(
            GetAllProjectsQuery request,
            CancellationToken cancellationToken)
        {
            var projects =
                await _projectRepository
                    .GetAllByUserIdAsync(
                        _currentUserService.UserId,
                        cancellationToken);

            var dtos = projects.Select(p =>
                new ProjectDto(
                    p.Id,
                    p.Name,
                    p.Description,
                    p.CreatedAt,
                    p.UpdatedAt,
                    p.Tasks.Count()
                ));

            return ApiResponse<IEnumerable<ProjectDto>>
                .SuccessResult(dtos);
        }
    }
}
