using AutoMapper;
using HRMS.Application.Common.Exceptions;
using HRMS.Application.Common.Interfaces;
using HRMS.Application.UseCases.Positions.Models;
using HRMS.Domain.Entities.Departments;
using HRMS.Domain.Entities.Positions;
using MediatR;

namespace HRMS.Application.UseCases.Positions.Commands.UpdatePosition
{
    public class UpdatePositionCommand : IRequest<PositionDto>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Salary { get; set; }
        public int MonthlyWorkingHours { get; set; }
        public Guid DepartmentId { get; set; }
    }

    public class UpdatePositionCommandHandle : IRequestHandler<UpdatePositionCommand, PositionDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public UpdatePositionCommandHandle(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PositionDto> Handle(UpdatePositionCommand request, CancellationToken cancellationToken)
        {
            Position maybePosition = await
                _context.Positions.FindAsync(new object[] { request.Id });

            ValidatePositionIsNotNull(request, maybePosition);

            Department maybeDepartment = await
             _context.Departments.FindAsync(new object[] { request.DepartmentId });

            ValidateDepartmentIsNotNull(request, maybeDepartment);

            maybePosition.Name = request.Name;
            maybePosition.Department = maybeDepartment;
            maybePosition.Salary = request.Salary;
            maybePosition.MonthlyWorkingHours = request.MonthlyWorkingHours;

            await _context.SaveChangesAsync(cancellationToken);

            return _mapper.Map<PositionDto>(maybePosition);
        }

        private void ValidatePositionIsNotNull(UpdatePositionCommand request, Position? maybePosition)
        {
            if (maybePosition is null)
            {
                throw new NotFoundException(nameof(Position), request.Id);
            }
        }
        private void ValidateDepartmentIsNotNull(UpdatePositionCommand request, Department? maybeDepartment)
        {
            if (maybeDepartment is null)
            {
                throw new NotFoundException(nameof(Department), request.DepartmentId);
            }
        }
    }
}
