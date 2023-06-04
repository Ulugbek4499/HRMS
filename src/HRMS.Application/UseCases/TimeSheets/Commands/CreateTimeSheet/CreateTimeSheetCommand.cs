using AutoMapper;
using HRMS.Application.Common.Exceptions;
using HRMS.Application.Common.Interfaces;
using HRMS.Application.UseCases.TimeSheets.Models;
using HRMS.Domain.Entities.Employees;
using HRMS.Domain.Entities.TimeSheets;
using MediatR;

namespace HRMS.Application.UseCases.TimeSheets.Commands.CreateTimeSheet
{
    public class CreateTimeSheetCommand : IRequest<TimeSheetDto>
    {
        public double WorkedHours { get; set; }
        public DateTimeOffset WorkingDay { get; set; }
        public Guid EmployeeId { get; set; }
    }

    public class CreateTimeSheetCommandHandler : IRequestHandler<CreateTimeSheetCommand, TimeSheetDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CreateTimeSheetCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        async Task<TimeSheetDto> IRequestHandler<CreateTimeSheetCommand, TimeSheetDto>.Handle(CreateTimeSheetCommand request, CancellationToken cancellationToken)
        {

            Employee maybeEmployee =
                _context.Employees.SingleOrDefault(p => p.Id.Equals(request.EmployeeId));

            var timeSheet = new TimeSheet
            {
                WorkingDay = request.WorkingDay,
                WorkedHours = request.WorkedHours,
                Employee = maybeEmployee,
            };


            timeSheet = _context.TimeSheets.Add(timeSheet).Entity;

            await _context.SaveChangesAsync(cancellationToken);

            return _mapper.Map<TimeSheetDto>(timeSheet);
        }
    }

}
