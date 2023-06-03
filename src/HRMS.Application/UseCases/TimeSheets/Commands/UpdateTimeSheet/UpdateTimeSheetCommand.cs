using AutoMapper;
using HRMS.Application.Common.Exceptions;
using HRMS.Application.Common.Interfaces;
using HRMS.Application.UseCases.TimeSheets.Models;
using HRMS.Domain.Entities.Employees;
using HRMS.Domain.Entities.TimeSheets;
using MediatR;

namespace HRMS.Application.UseCases.TimeSheets.Commands.UpdateTimeSheet
{
    public class UpdateTimeSheetCommand : IRequest<TimeSheetDto>
    {
        public Guid Id { get; set; }
        public Guid EmployeeId { get; set; }
        public DateTimeOffset WorkingDay { get; set; }
        public double WorkedHours { get; set; }
    }

    public class UpdateTimeSheetCommandHandler : IRequestHandler<UpdateTimeSheetCommand, TimeSheetDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public UpdateTimeSheetCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        async Task<TimeSheetDto> IRequestHandler<UpdateTimeSheetCommand, TimeSheetDto>.Handle(UpdateTimeSheetCommand request, CancellationToken cancellationToken)
        {
            TimeSheet maybeTimeSheet = await
                _context.TimeSheets.FindAsync(new object[] { request.Id });

            ValidateTimeSheetIsNotNull(request, maybeTimeSheet);

            Employee maybeEmployee = _context.Employees
                .SingleOrDefault(p => p.Id.Equals(request.EmployeeId));

            ValidateEmployeeAreNotNull(request, maybeEmployee);

            maybeTimeSheet.WorkedHours = request.WorkedHours;
            maybeTimeSheet.WorkingDay = request.WorkingDay;
            maybeTimeSheet.Employee = maybeEmployee;

            await _context.SaveChangesAsync(cancellationToken);

            return _mapper.Map<TimeSheetDto>(maybeTimeSheet);
        }

        private static void ValidateEmployeeAreNotNull(UpdateTimeSheetCommand request, Employee? maybeEmployee)
        {
            if (maybeEmployee is null)
            {
                throw new NotFoundException(nameof(Employee), request.EmployeeId);
            }
        }

        private static void ValidateTimeSheetIsNotNull(UpdateTimeSheetCommand request, TimeSheet maybeTimeSheet)
        {
            if (maybeTimeSheet == null)
            {
                throw new NotFoundException(nameof(TimeSheet), request.Id);
            }
        }
    }
}
