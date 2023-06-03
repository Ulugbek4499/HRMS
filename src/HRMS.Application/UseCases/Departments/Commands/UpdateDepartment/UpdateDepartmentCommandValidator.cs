using FluentValidation;

namespace HRMS.Application.UseCases.Departments.Commands.UpdateDepartment
{
    public class UpdateDepartmentCommandValidator : AbstractValidator<UpdateDepartmentCommand>
    {
        public UpdateDepartmentCommandValidator()
        {
            RuleFor(d => d.Name)
                .NotEmpty()
                .WithMessage("User name is required.");
        }
    }
}
