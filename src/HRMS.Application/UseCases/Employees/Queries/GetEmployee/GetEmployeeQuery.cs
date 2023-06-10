using AutoMapper;
using HRMS.Application.Common.Exceptions;
using HRMS.Application.Common.Interfaces;
using HRMS.Application.UseCases.Employees.Models;
using HRMS.Domain.Entities.Employees;
using MediatR;

namespace HRMS.Application.UseCases.Employees.Queries.GetEmployee
{
    public record GetEmployeeQuery(Guid employeeId) : IRequest<EmployeeDto>;

    public class GetEmployeeQueryHandler : IRequestHandler<GetEmployeeQuery, EmployeeDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetEmployeeQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<EmployeeDto> Handle(GetEmployeeQuery request, CancellationToken cancellationToken)
        {

            Employee maybeEmployee = await
                _context.Employees.FindAsync(new object[] { request.employeeId });

            ValidateEmployeeIsNotNull(request, maybeEmployee);

            return _mapper.Map<EmployeeDto>(maybeEmployee);
        }

        private void ValidateEmployeeIsNotNull(GetEmployeeQuery request, Employee? maybeEmployee)
        {
            if (maybeEmployee is null)
            {
                throw new NotFoundException(nameof(Employee), request.employeeId);
            }
        }
    }
}
