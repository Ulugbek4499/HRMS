using AutoMapper;
using HRMS.Application.Common.Exceptions;
using HRMS.Application.Common.Interfaces;
using HRMS.Application.UseCases.Departments.Models;
using HRMS.Application.UseCases.TimeSheets.Models;
using HRMS.Domain.Entities.Departments;
using HRMS.Domain.Entities.Salaries;
using HRMS.Domain.Entities.TimeSheets;
using MediatR;

namespace HRMS.Application.UseCases.TimeSheets.Queries.GetTimeSheet
{
    public record GetTimeSheetQuery(Guid timeSheetId) : IRequest<TimeSheetDto> { }

    public class GetTimeSheetQueryHandler : IRequestHandler<GetTimeSheetQuery, TimeSheetDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public GetTimeSheetQueryHandler(
           IApplicationDbContext context,
           IMapper mapper)
             {
            _context = context;
            _mapper = mapper;
             }
        public async Task<TimeSheetDto> Handle(GetTimeSheetQuery request, CancellationToken cancellationToken)
        {

            TimeSheet maybetimeSheet = await _context.TimeSheets
             .FindAsync(new object[] { request.timeSheetId });

            ValidateTimeSheetIsNotNull(request, maybetimeSheet);

            return _mapper.Map<TimeSheetDto>(maybetimeSheet);
        }

        private void ValidateTimeSheetIsNotNull(GetTimeSheetQuery request, TimeSheet? maybetimeSheet)
        {
            if (maybetimeSheet == null)
            {
                throw new NotFoundException(nameof(Salary), request.timeSheetId);
            }
        }
    }
}
