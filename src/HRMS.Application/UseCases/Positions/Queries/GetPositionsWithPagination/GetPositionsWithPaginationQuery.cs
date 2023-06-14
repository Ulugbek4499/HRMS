using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using HRMS.Application.Common.Interfaces;
using HRMS.Application.Common.Models;
using HRMS.Application.UseCases.Positions.Models;
using HRMS.Domain.Entities.Positions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Application.UseCases.Positions.Queries.GetPositionsWithPagination
{
    public record GetPositionsWithPaginationQuery : IRequest<PaginatedList<PositionDto>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;

    }

    public class GetPositionsWithPaginationQueryHandler : IRequestHandler<GetPositionsWithPaginationQuery, PaginatedList<PositionDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetPositionsWithPaginationQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PaginatedList<PositionDto>> Handle(GetPositionsWithPaginationQuery request, CancellationToken cancellationToken)
        {
            Position[] positions = await _context.Positions.ToArrayAsync();

            List<PositionDto> dtos = _mapper.Map<PositionDto[]>(positions).ToList();

            PaginatedList<PositionDto> paginatedList =
                PaginatedList<PositionDto>.CreateAsync(
                    dtos, request.PageNumber, request.PageSize);

            return paginatedList;
        }
    }
}
