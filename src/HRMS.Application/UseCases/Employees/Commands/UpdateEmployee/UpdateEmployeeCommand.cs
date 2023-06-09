using AutoMapper;
using HRMS.Application.Common.Exceptions;
using HRMS.Application.Common.Interfaces;
using HRMS.Application.UseCases.Employees.Models;
using HRMS.Domain.Entities.Employees;
using HRMS.Domain.Entities.Positions;
using MediatR;

namespace HRMS.Application.UseCases.Employees.Commands.UpdateEmployee
{
    public class UpdateEmployeeCommand : IRequest<EmployeeDto>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public Guid PositionId { get; set; }
    }

    public class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand, EmployeeDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public UpdateEmployeeCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<EmployeeDto> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
            Employee maybeEmployee = await
                _context.Employees.FindAsync(new object[] { request.Id });

            ValidateEmployeeIsNotNull(request, maybeEmployee);

            Position maybePosition =
                _context.Positions.SingleOrDefault(p => p.Id.Equals(request.PositionId));

            ValidatePositionsAreNotNull(request, maybePosition);

            maybeEmployee.Name = request.Name;
            maybeEmployee.PhoneNumber = request.PhoneNumber;
            maybeEmployee.Position = maybePosition;

            await _context.SaveChangesAsync(cancellationToken);

            return _mapper.Map<EmployeeDto>(maybeEmployee);
        }

        private void ValidatePositionsAreNotNull(UpdateEmployeeCommand request, Position? maybePosition)
        {
            if (maybePosition is null)
            {
                throw new NotFoundException(nameof(Position), request.PositionId);
            }
        }

        private void ValidateEmployeeIsNotNull(UpdateEmployeeCommand request, Employee? maybeEmployee)
        {
            if (maybeEmployee == null)
            {
                throw new AlreadyExistsException(nameof(Employee), request.Name);
            }
        }
    }
}
