using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTaskManager.Application.Features.Tasks.Commands.CreateTask
{
    public class CreateTaskCommandValidator
    : AbstractValidator<CreateTaskCommand>
    {
        public CreateTaskCommandValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .MaximumLength(300);

            RuleFor(x => x.Description)
                .MaximumLength(2000);

            RuleFor(x => x.ProjectId)
                .NotEmpty();

            RuleFor(x => x.Priority)
                .IsInEnum();
        }
    }
}
