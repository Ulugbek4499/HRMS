using FluentValidation;

namespace HRMS.Application.UseCases.Employees.Commands.UpdateEmployee
{
    public class UpdateEmployeeCommandValidator : AbstractValidator<UpdateEmployeeCommand>
    {
        public UpdateEmployeeCommandValidator()
        {
            RuleFor(t => t.Id).NotEmpty()
                .NotEqual((Guid)default)
                .WithMessage("Id is required.");

            RuleFor(t => t.PositionId).NotEmpty()
                .NotEqual((Guid)default)
                .WithMessage("Position id is required.");

            RuleFor(p => p.FirstName)
                .NotEmpty()
                .MaximumLength(50)
                .WithMessage("FirstName is required.");

            RuleFor(p => p.LastName)
               .NotEmpty()
               .MaximumLength(50)
               .WithMessage("Lastname is required.");
        }
    }
}
