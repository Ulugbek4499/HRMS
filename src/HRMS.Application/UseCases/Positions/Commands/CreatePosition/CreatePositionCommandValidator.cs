using FluentValidation;

namespace HRMS.Application.UseCases.Positions.Commands.CreatePosition
{
    public class CreatePositionCommandValidator : AbstractValidator<CreatePositionCommand>
    {
        public CreatePositionCommandValidator()
        {
            RuleFor(d => d.Name).NotEmpty()
                .MaximumLength(50)
                .WithMessage("Position name is required");

            RuleFor(t => t.Salary).NotEqual((decimal)default)
               .GreaterThan(0).WithMessage("Salary is required.");

            RuleFor(t => t.MonthlyWorkingHours).NotEqual((int)default)
             .GreaterThan(0).WithMessage("Monthly working hours is required.");

            RuleFor(t => t.DepartmentId).NotEmpty()
               .NotEqual((Guid)default)
               .WithMessage("Department id is required.");
        }
    }
}
