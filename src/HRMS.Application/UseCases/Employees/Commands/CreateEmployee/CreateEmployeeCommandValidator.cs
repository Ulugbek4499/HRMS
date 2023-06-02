using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace HRMS.Application.UseCases.Employees.Commands.CreateEmployee
{
    public class CreateEmployeeCommandValidator:AbstractValidator<CreateEmployeeCommand>
    {
        public CreateEmployeeCommandValidator()
        {
            RuleFor(t => t.PositionId).NotEmpty()
              .NotEqual((Guid)default)
              .WithMessage("Position id is required.");

            RuleFor(d => d.FirstName).NotEmpty().MaximumLength(50).WithMessage("First name is required");

            RuleFor(d => d.LastName).NotEmpty().MaximumLength(50).WithMessage("Last name is required");
        }
    }
}
