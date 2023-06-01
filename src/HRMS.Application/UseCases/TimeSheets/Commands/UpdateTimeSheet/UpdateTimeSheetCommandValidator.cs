using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace HRMS.Application.UseCases.TimeSheets.Commands.UpdateTimeSheet
{
    public class UpdateTimeSheetCommandValidator:AbstractValidator<UpdateTimeSheetCommand>
    {
        public UpdateTimeSheetCommandValidator()
        {
            RuleFor(t => t.Id).NotEmpty()
                .NotEqual((Guid)default)
                .WithMessage("Id is required.");

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
