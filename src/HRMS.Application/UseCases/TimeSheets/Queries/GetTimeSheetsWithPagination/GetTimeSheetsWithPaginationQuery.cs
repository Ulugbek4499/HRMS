using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using HRMS.Application.Common.Interfaces;
using HRMS.Application.Common.Models;
using HRMS.Application.UseCases.TimeSheets.Models;
using HRMS.Domain.Entities.TimeSheets;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Application.UseCases.TimeSheets.Queries.GetTimeSheetsWithPagination
{
    public record GetTimeSheetsWithPaginationQuery : IRequest<PaginatedList<TimeSheetDto>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;

    }

    public class GetTimeSheetsWithPaginationQueryHandler : IRequestHandler<GetTimeSheetsWithPaginationQuery, PaginatedList<TimeSheetDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetTimeSheetsWithPaginationQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PaginatedList<TimeSheetDto>> Handle(GetTimeSheetsWithPaginationQuery request, CancellationToken cancellationToken)
        {
            TimeSheet[] timeSheets = await _context.TimeSheets.ToArrayAsync(cancellationToken);

            List<TimeSheetDto> dtos = _mapper.Map<TimeSheetDto[]>(timeSheets).ToList();

            PaginatedList<TimeSheetDto> paginatedList =
                PaginatedList<TimeSheetDto>.CreateAsync(
                    dtos, request.PageNumber, request.PageSize);

            return paginatedList;
        }
    }
}
