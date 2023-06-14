using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using HRMS.Application.Common.Interfaces;
using HRMS.Application.Common.Models;
using HRMS.Application.UseCases.Employees.Models;
using HRMS.Domain.Entities.Employees;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Application.UseCases.Employees.Queries.GetEmployeesWithPagination
{

    public record GetEmployeesWithPaginationQuery : IRequest<PaginatedList<EmployeeDto>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;

    }

    public class GetEmployeesWithPaginationQueryHandler : IRequestHandler<GetEmployeesWithPaginationQuery, PaginatedList<EmployeeDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetEmployeesWithPaginationQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PaginatedList<EmployeeDto>> Handle(GetEmployeesWithPaginationQuery request, CancellationToken cancellationToken)
        {
            Employee[] employees = await _context.Employees.ToArrayAsync();

            List<EmployeeDto> dtos = _mapper.Map<EmployeeDto[]>(employees).ToList();

            PaginatedList<EmployeeDto> paginatedList =
                PaginatedList<EmployeeDto>.CreateAsync(
                    dtos, request.PageNumber, request.PageSize);

            return paginatedList;
        }
    }
}
