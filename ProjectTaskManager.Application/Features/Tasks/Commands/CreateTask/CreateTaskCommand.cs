using MediatR;
using ProjectTaskManager.Application.Common.Models;
using ProjectTaskManager.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTaskManager.Application.Features.Tasks.Commands.CreateTask
{
    public record CreateTaskCommand(
    string Title,
    string? Description,
    TaskPriority Priority,
    DateTime? DueDate,
    Guid ProjectId)
    : IRequest<ApiResponse<TaskDto>>;
}
