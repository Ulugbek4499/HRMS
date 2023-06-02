using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using HRMS.Application.Common.Interfaces;
using HRMS.Application.UseCases.Employees.Models;
using HRMS.Domain.Entities.Employees;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Application.UseCases.Employees.Queries.GetEmployees
{
    public record GetEmployeesQuery : IRequest<EmployeeDto[]>;

    public class GetEmployeesQueryHandler : IRequestHandler<GetEmployeesQuery, EmployeeDto[]>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetEmployeesQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<EmployeeDto[]> Handle(GetEmployeesQuery request, CancellationToken cancellationToken)
        {
            Employee[] employees = await _context.Employees.ToArrayAsync();

            return _mapper.Map<EmployeeDto[]>(employees);
        }
    }
}
