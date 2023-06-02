using AutoMapper;
using HRMS.Application.Common.Exceptions;
using HRMS.Application.Common.Interfaces;
using HRMS.Application.UseCases.Positions.Models;
using HRMS.Domain.Entities.Departments;
using HRMS.Domain.Entities.Positions;
using HRMS.Domain.Entities.Salaries;
using MediatR;

namespace HRMS.Application.UseCases.Positions.Commands.UpdatePosition
{
    public class UpdatePositionCommand:IRequest<PositionDto>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid SalaryId { get; set; }
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

            Salary maybeSalary = await
                _context.Salaries.FindAsync(new object[] { request.SalaryId });

            ValidateSalaryIsNotNull(request, maybeSalary);

            Department maybeDepartment = await
             _context.Departments.FindAsync(new object[] { request.DepartmentId });
           
            ValidateDepartmentIsNotNull(request, maybeDepartment);

            maybePosition.Name = request.Name;
            maybePosition.Department = maybeDepartment;
            maybePosition.Salary = maybeSalary;

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

        private void ValidateSalaryIsNotNull(UpdatePositionCommand request, Salary? maybeSalary)
        {
            if (maybeSalary is null)
            {
                throw new NotFoundException(nameof(Salary), request.SalaryId);
            }
        }
    }
}
