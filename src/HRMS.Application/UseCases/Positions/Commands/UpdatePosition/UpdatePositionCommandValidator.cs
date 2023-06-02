using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace HRMS.Application.UseCases.Positions.Commands.UpdatePosition
{
    public class UpdatePositionCommandValidator:AbstractValidator<UpdatePositionCommand>
    {
        public UpdatePositionCommandValidator()
        {
            RuleFor(t => t.Id).NotEmpty()
          .NotEqual((Guid)default)
          .WithMessage("Id is required.");

            RuleFor(t => t.DepartmentId).NotEmpty()
                .NotEqual((Guid)default)
                .WithMessage("Department id is required.");

            RuleFor(t => t.SalaryId).NotEmpty()
                .NotEqual((Guid)default)
                .WithMessage("Salary id is required.");

            RuleFor(p => p.Name)
               .NotEmpty()
               .MaximumLength(50)
               .WithMessage("Position name is required.");

        }
    }
}
