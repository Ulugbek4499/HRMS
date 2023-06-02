using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using HRMS.Application.Common.Interfaces;
using HRMS.Application.UseCases.Positions.Models;
using HRMS.Domain.Entities.Positions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Application.UseCases.Positions.Queries.GetPositions
{
    public record GetPositionsQuery : IRequest<PositionDto[]>;

    public class GetPositionsQueryHandler : IRequestHandler<GetPositionsQuery, PositionDto[]>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetPositionsQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PositionDto[]> Handle(GetPositionsQuery request, CancellationToken cancellationToken)
        {
            Position[] Positions = await _context.Positions.ToArrayAsync();

            return _mapper.Map<PositionDto[]>(Positions);
        }
    }
}
