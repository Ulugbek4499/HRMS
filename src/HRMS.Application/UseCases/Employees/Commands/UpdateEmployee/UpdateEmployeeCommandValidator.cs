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

            RuleFor(p => p.Name)
                .NotEmpty()
                .MaximumLength(50)
                .WithMessage("Name is required.");


            RuleFor(u => u.PhoneNumber)
                .Must(ValidatePhone)
                .Length(13).WithMessage("Please enter valid phone number like +998901234567");
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
