using AutoMapper;
using HRMS.Application.Common.Exceptions;
using HRMS.Application.Common.Interfaces;
using HRMS.Application.UseCases.Employees.Models;
using HRMS.Domain.Entities.Employees;
using HRMS.Domain.Entities.Positions;
using MediatR;

namespace HRMS.Application.UseCases.Employees.Commands.CreateEmployee
{
    public class CreateEmployeeCommand:IRequest<EmployeeDto>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
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
            Position maybePostion =
                _context.Positions.SingleOrDefault(p => p.Id.Equals(request.PositionId));

            ValidatePositionIsNotNull(request, maybePostion);

            var employee=new Employee()
            {
                FirstName= request.FirstName,
                LastName= request.LastName,
               
            };
            
            employee = _context.Employees.Add(employee).Entity;
            await _context.SaveChangesAsync(cancellationToken);

            return _mapper.Map<EmployeeDto>(employee);
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
