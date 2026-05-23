using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTaskManager.Application.Features.Tasks.Commands.GetTaskByIdInternal
{
    public record GetTaskByIdInternalQuery(
    Guid Id
) : IRequest<TaskDto>;
}
