using AutoMapper;
using HRMS.Application.Common.Interfaces;
using HRMS.Application.UseCases.Departments.Models;
using HRMS.Domain.Entities.Departments;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Application.UseCases.Departments.Queries.GetDepartments
{
    public record GetDepartmentsQuery : IRequest<DepartmentDto[]>;

    public class GetDepartmentsQueryHandler : IRequestHandler<GetDepartmentsQuery, DepartmentDto[]>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetDepartmentsQueryHandler(
            IApplicationDbContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<DepartmentDto[]> Handle(GetDepartmentsQuery request, CancellationToken cancellationToken)
        {
            Department[] departments = await _context.Departments.ToArrayAsync();

            return _mapper.Map<DepartmentDto[]>(departments);
        }
    }
}
