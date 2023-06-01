using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace HRMS.Application.UseCases.Salaries.Commands.UpdateSalary
{
    public class UpdateSalaryCommandValidator:AbstractValidator<UpdateSalaryCommand>
    {
        public UpdateSalaryCommandValidator()
        {
            RuleFor(t => t.Id).NotEmpty()
             .NotEqual((Guid)default)
             .WithMessage("Id is required.");

            RuleFor(t => t.PositionId).NotEmpty()
                .NotEqual((Guid)default)
                .WithMessage("Position id is required.");

            RuleFor(t => t.MonthlyWorkingHours).NotEqual((int)default)
                .GreaterThan(0).WithMessage("Worked Hours is required.");

            RuleFor(t => t.SalaryAmount).NotEqual((decimal)default)
              .GreaterThan(0).WithMessage("Salary Amount  is required.");

        }
    }
}
