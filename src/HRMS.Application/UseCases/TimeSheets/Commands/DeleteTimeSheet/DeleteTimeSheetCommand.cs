using AutoMapper;
using HRMS.Application.Common.Exceptions;
using HRMS.Application.Common.Interfaces;
using HRMS.Application.UseCases.TimeSheets.Models;
using HRMS.Domain.Entities.TimeSheets;
using MediatR;

namespace HRMS.Application.UseCases.TimeSheets.Commands.DeleteTimeSheet
{
    public record DeleteTimeSheetCommand(Guid timeSheetId) : IRequest<TimeSheetDto>;

    public class DeleteTimeSheetCommandHandler : IRequestHandler<DeleteTimeSheetCommand, TimeSheetDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public DeleteTimeSheetCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        async Task<TimeSheetDto> IRequestHandler<DeleteTimeSheetCommand, TimeSheetDto>.Handle(DeleteTimeSheetCommand request, CancellationToken cancellationToken)
        {
            TimeSheet maybeTimeSheet = await
                  _context.TimeSheets.FindAsync(new object[] { request.timeSheetId });

            ValidateTimeSheetIsNotNull(request, maybeTimeSheet);

            _context.TimeSheets.Remove(maybeTimeSheet);

            await _context.SaveChangesAsync(cancellationToken);

            return _mapper.Map<TimeSheetDto>(maybeTimeSheet);
        }

        private static void ValidateTimeSheetIsNotNull(DeleteTimeSheetCommand request, TimeSheet maybeTimeSheet)
        {
            if (maybeTimeSheet is null)
            {
                throw new NotFoundException(nameof(TimeSheet), request.timeSheetId);
            }
        }
    }
}
