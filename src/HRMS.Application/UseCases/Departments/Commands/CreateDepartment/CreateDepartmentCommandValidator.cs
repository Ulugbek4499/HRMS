using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace HRMS.Application.UseCases.Departments.Commands.CreateDepartment
{
    public class CreateDepartmentCommandValidator:AbstractValidator<CreateDepartmentCommand>
    {
        public CreateDepartmentCommandValidator()
        {
            RuleFor(d => d.Name).NotEmpty().MaximumLength(50).WithMessage("Department name is required");
        }
    }
}
