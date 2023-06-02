using AutoMapper;
using HRMS.Application.Common.Interfaces;
using HRMS.Application.UseCases.TimeSheets.Models;
using HRMS.Domain.Entities.TimeSheets;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Application.UseCases.TimeSheets.Queries.GetTimeSheets
{
    public record GetTimeSheetsQuery : IRequest<TimeSheetDto[]>;

    public class GetTimeSheetsQueryHandler : IRequestHandler<GetTimeSheetsQuery, TimeSheetDto[]>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetTimeSheetsQueryHandler(
            IApplicationDbContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<TimeSheetDto[]> Handle(GetTimeSheetsQuery request, CancellationToken cancellationToken)
        {
            TimeSheet[] timeSheets = await _context.TimeSheets.ToArrayAsync();

            return _mapper.Map<TimeSheetDto[]>(timeSheets);
        }
    }
}
