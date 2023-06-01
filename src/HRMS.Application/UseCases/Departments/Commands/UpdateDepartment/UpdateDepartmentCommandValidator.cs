using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace HRMS.Application.UseCases.Departments.Commands.UpdateDepartment
{
    public class UpdateDepartmentCommandValidator:AbstractValidator<UpdateDepartmentCommand>
    {
        public UpdateDepartmentCommandValidator()
        {
            RuleFor(d => d.Name)
                .NotEmpty()
                .WithMessage("User name is required.");

            RuleFor(d => d.Positions)
                .ForEach(r => r.NotEqual((Guid)default))
                .WithMessage("Please enter valid position");
        }
    }
}
