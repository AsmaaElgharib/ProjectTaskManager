using MediatR;
using ProjectTaskManager.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTaskManager.Application.Features.Tasks.Queries.GetTasksByProject
{
    public record GetTasksByProjectQuery(
    Guid ProjectId)
    : IRequest<ApiResponse<IEnumerable<TaskDto>>>;
}
