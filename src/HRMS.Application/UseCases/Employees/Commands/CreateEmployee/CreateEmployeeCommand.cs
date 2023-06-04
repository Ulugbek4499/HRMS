using AutoMapper;
using HRMS.Application.Common.Exceptions;
using HRMS.Application.Common.Interfaces;
using HRMS.Application.UseCases.Employees.Models;
using HRMS.Domain.Entities.Employees;
using HRMS.Domain.Entities.Positions;
using MediatR;

namespace HRMS.Application.UseCases.Employees.Commands.CreateEmployee
{
    public class CreateEmployeeCommand : IRequest<EmployeeDto>
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; } = "+998";
        public Guid PositionId { get; set; }
    }

    public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, EmployeeDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CreateEmployeeCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<EmployeeDto> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            bool isExists = _context.Employees.Any(u => u.PhoneNumber == request.PhoneNumber);

            ValidateEmployeeNotExists(request, isExists);

            Position maybePostion =
                _context.Positions.SingleOrDefault(p => p.Id.Equals(request.PositionId));

            ValidatePositionIsNotNull(request, maybePostion);

            var employee = new Employee()
            {
                Name = request.Name,
                PhoneNumber = request.PhoneNumber,
                Position = maybePostion
            };

            employee = _context.Employees.Add(employee).Entity;
            await _context.SaveChangesAsync(cancellationToken);

            return _mapper.Map<EmployeeDto>(employee);
        }

        private void ValidateEmployeeNotExists(CreateEmployeeCommand request, bool isExists)
        {
            if (isExists)
            {
                throw new AlreadyExistsException(nameof(Employee), request.PhoneNumber);
            }
        }

        private void ValidatePositionIsNotNull(CreateEmployeeCommand request, Position? maybePostion)
        {
            if (maybePostion == null)
            {
                throw new NotFoundException(nameof(Position), request.PositionId);
            }
        }
    }
}
