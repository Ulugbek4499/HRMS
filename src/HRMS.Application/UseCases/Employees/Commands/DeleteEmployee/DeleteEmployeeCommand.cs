using AutoMapper;
using HRMS.Application.Common.Exceptions;
using HRMS.Application.Common.Interfaces;
using HRMS.Application.UseCases.Employees.Models;
using HRMS.Domain.Entities.Employees;
using MediatR;

namespace HRMS.Application.UseCases.Employees.Commands.DeleteEmployee
{
    public record DeleteEmployeeCommand(Guid emploeeId):IRequest<EmployeeDto>;

    public class DeleteEmployeeCommandHandler : IRequestHandler<DeleteEmployeeCommand, EmployeeDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public DeleteEmployeeCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<EmployeeDto> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
        {
            Employee maybeEmployee = await
                  _context.Employees.FindAsync(new object[] { request.emploeeId });

            ValidateDepartmentIsNotNull(request, maybeEmployee);

            _context.Employees.Remove(maybeEmployee);

            await _context.SaveChangesAsync(cancellationToken);

            return _mapper.Map<EmployeeDto>(maybeEmployee);
        }

        private static void ValidateDepartmentIsNotNull(DeleteEmployeeCommand request, Employee maybeEmployee)
        {
            if (maybeEmployee is null)
            {
                throw new NotFoundException(nameof(Employee), request.emploeeId);
            }
        }
    }
}
