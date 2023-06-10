using AutoMapper;
using HRMS.Application.Common.Exceptions;
using HRMS.Application.Common.Interfaces;
using HRMS.Application.UseCases.Positions.Models;
using HRMS.Domain.Entities.Positions;
using MediatR;

namespace HRMS.Application.UseCases.Positions.Commands.DeletePosition
{
    public record DeletePositionCommand(Guid positionId) : IRequest<PositionDto>;

    public class DeletePositionCommandHandler : IRequestHandler<DeletePositionCommand, PositionDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public DeletePositionCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PositionDto> Handle(DeletePositionCommand request, CancellationToken cancellationToken)
        {
            Position maybePosition = await
                   _context.Positions.FindAsync(new object[] { request.positionId });

            ValidatePositionIsNotNull(request, maybePosition);

            _context.Positions.Remove(maybePosition);

            await _context.SaveChangesAsync(cancellationToken);

            return _mapper.Map<PositionDto>(maybePosition);
        }

        private void ValidatePositionIsNotNull(DeletePositionCommand request, Position? maybePosition)
        {
            if (maybePosition is null)
            {
                throw new NotFoundException(nameof(Position), request.positionId);
            }
        }
    }
}
