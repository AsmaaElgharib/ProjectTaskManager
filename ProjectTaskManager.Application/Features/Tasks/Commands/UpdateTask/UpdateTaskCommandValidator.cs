using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTaskManager.Application.Features.Tasks.Commands.UpdateTask
{
    public class UpdateTaskCommandValidator
    : AbstractValidator<UpdateTaskCommand>
    {
        public UpdateTaskCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();

            RuleFor(x => x.Title)
                .NotEmpty()
                .MaximumLength(300);

            RuleFor(x => x.Description)
                .MaximumLength(2000);

            RuleFor(x => x.Status)
                .IsInEnum();

            RuleFor(x => x.Priority)
                .IsInEnum();
        }
    }
}
