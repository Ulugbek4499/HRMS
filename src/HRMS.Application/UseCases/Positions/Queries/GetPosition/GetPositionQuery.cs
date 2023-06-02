using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using HRMS.Application.Common.Exceptions;
using HRMS.Application.Common.Interfaces;
using HRMS.Application.UseCases.Employees.Models;
using HRMS.Application.UseCases.Positions.Models;
using HRMS.Domain.Entities.Employees;
using HRMS.Domain.Entities.Positions;
using MediatR;

namespace HRMS.Application.UseCases.Positions.Queries.GetPosition
{
    public record GetPositionQuery(Guid positionId) : IRequest<PositionDto>;

    public class GetPositionQueryHandler : IRequestHandler<GetPositionQuery, PositionDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetPositionQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PositionDto> Handle(GetPositionQuery request, CancellationToken cancellationToken)
        {
            Position maybePosition = await
                _context.Positions.FindAsync(new object[] { request.positionId });

            ValidatePositionIsNotNull(request, maybePosition);

            return _mapper.Map<PositionDto>(maybePosition);
        }

        private void ValidatePositionIsNotNull(GetPositionQuery request, Position? maybePosition)
        {
            if (maybePosition is null)
            {
                throw new NotFoundException(nameof(Position), request.positionId);
            }
        }

}
