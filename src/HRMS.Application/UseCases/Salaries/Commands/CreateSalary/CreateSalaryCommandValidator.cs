using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace HRMS.Application.UseCases.Salaries.Commands.CreateSalary
{
    public class CreateSalaryCommandValidator:AbstractValidator<CreateSalaryCommand>
    {
        public CreateSalaryCommandValidator()
        {
            RuleFor(t => t.PositionId).NotEmpty()
               .NotEqual((Guid)default)
               .WithMessage("Position id is required.");

            RuleFor(t => t.SalaryAmount).NotEqual((decimal)default)
                .GreaterThan(0).WithMessage("Salary amount is required.");

            RuleFor(t => t.MonthlyWorkingHours).NotEqual((int)default)
                .WithMessage("Monthly Working Hours are required.");
        }
    }
}
