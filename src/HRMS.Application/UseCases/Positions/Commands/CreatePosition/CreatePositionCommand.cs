using AutoMapper;
using HRMS.Application.Common.Exceptions;
using HRMS.Application.Common.Interfaces;
using HRMS.Application.UseCases.Positions.Models;
using HRMS.Domain.Entities.Departments;
using HRMS.Domain.Entities.Positions;
using MediatR;

namespace HRMS.Application.UseCases.Positions.Commands.CreatePosition
{
    public class CreatePositionCommand : IRequest<PositionDto>
    {
        public string Name { get; set; }
        public decimal Salary { get; set; }
        public int MonthlyWorkingHours { get; set; }
        public Guid DepartmentId { get; set; }
    }

    public class CreatePositionCommandHanlder : IRequestHandler<CreatePositionCommand, PositionDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CreatePositionCommandHanlder(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PositionDto> Handle(CreatePositionCommand request, CancellationToken cancellationToken)
        {
            Department maybeDepartment = await
                _context.Departments.FindAsync(new object[] { request.DepartmentId });

            ValidateDepartmentIsNotNull(request, maybeDepartment);

            var position = new Position()
            {
                Name = request.Name,
                Salary = request.Salary,
                MonthlyWorkingHours = request.MonthlyWorkingHours,
                Department = maybeDepartment
            };

            position = _context.Positions.Add(position).Entity;
            await _context.SaveChangesAsync(cancellationToken);

            return _mapper.Map<PositionDto>(position);
        }

        private void ValidateDepartmentIsNotNull(CreatePositionCommand request, Department? maybeDepartment)
        {
            if (maybeDepartment is null)
            {
                throw new NotFoundException(nameof(Department), request.DepartmentId);
            }
        }
    }
}
