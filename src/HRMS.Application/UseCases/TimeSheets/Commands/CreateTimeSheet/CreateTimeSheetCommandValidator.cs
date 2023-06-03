using FluentValidation;

namespace HRMS.Application.UseCases.TimeSheets.Commands.CreateTimeSheet
{
    public class CreateTimeSheetCommandValidator : AbstractValidator<CreateTimeSheetCommand>
    {
        public CreateTimeSheetCommandValidator()
        {
            RuleFor(t => t.EmployeeId).NotEmpty()
                .NotEqual((Guid)default)
                .WithMessage("Employee id is required.");

            RuleFor(t => t.WorkedHours).NotEqual((double)default)
                .GreaterThan(0).WithMessage("Worked Hours is required.");

            RuleFor(t => t.WorkingDay).NotEqual((DateTimeOffset)default)
                .WithMessage("Working date is required.");
        }
    }
}
