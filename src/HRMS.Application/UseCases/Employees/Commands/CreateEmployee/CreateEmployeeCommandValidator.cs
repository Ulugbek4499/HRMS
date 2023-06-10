using FluentValidation;

namespace HRMS.Application.UseCases.Employees.Commands.CreateEmployee
{
    public class CreateEmployeeCommandValidator : AbstractValidator<CreateEmployeeCommand>
    {
        public CreateEmployeeCommandValidator()
        {
            RuleFor(t => t.PositionId).NotEmpty()
              .NotEqual((Guid)default)
              .WithMessage("Position id is required.");

            RuleFor(u => u.PhoneNumber)
                .Must(ValidatePhone)
                .Length(13)
                .WithMessage("Please enter valid phone number like +998901234567");

            RuleFor(d => d.Name)
                .NotEmpty()
                .MaximumLength(50)
                .WithMessage("Name is required");
        }

        private bool ValidatePhone(string phone)
        {
            bool isTrue = phone.StartsWith("+998");

            for (int i = 1; i < phone.Length; i++)
            {
                if (!char.IsNumber(phone[i]))
                {
                    phone.Remove(i, 1);
                }
            }

            return isTrue;
        }
    }
}
