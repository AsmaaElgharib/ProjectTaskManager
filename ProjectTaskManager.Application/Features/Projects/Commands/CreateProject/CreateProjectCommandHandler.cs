using MediatR;
using Microsoft.Extensions.Logging;
using ProjectTaskManager.Application.Common.Models;
using ProjectTaskManager.Application.Features.Projects.Dtos;

namespace ProjectTaskManager.Application.Features.Projects.Commands.CreateProject
{
    public class CreateProjectCommandHandler(ILogger<CreateProjectCommandHandler> logger) : IRequestHandler<CreateProjectCommand, ApiResponse<ProjectDto>>
    {
        public Task<ApiResponse<ProjectDto>> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
