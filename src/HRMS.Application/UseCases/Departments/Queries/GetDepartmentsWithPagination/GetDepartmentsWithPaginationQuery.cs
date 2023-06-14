using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using HRMS.Application.Common.Interfaces;
using HRMS.Application.Common.Models;
using HRMS.Application.UseCases.Departments.Models;
using HRMS.Domain.Entities.Departments;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Application.UseCases.Departments.Queries.GetDepartmentsWithPagination
{
    public record GetDepartmentsWithPaginationQuery:IRequest<PaginatedList<DepartmentDto>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;

    }

    public class GetDepartmentsWithPaginationQueryHandler : IRequestHandler<GetDepartmentsWithPaginationQuery, PaginatedList<DepartmentDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetDepartmentsWithPaginationQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PaginatedList<DepartmentDto>> Handle(GetDepartmentsWithPaginationQuery request, CancellationToken cancellationToken)
        {
            Department[] departments = await _context.Departments.ToArrayAsync();

            List<DepartmentDto> dtos = _mapper.Map<DepartmentDto[]>(departments).ToList();

            PaginatedList<DepartmentDto> paginatedList =
                PaginatedList<DepartmentDto>.CreateAsync(
                    dtos, request.PageNumber, request.PageSize);

            return paginatedList;
        }
    }
}
