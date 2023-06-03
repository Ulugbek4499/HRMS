using AutoMapper;
using HRMS.Application.Common.Exceptions;
using HRMS.Application.Common.Interfaces;
using HRMS.Application.UseCases.Departments.Models;
using HRMS.Domain.Entities.Departments;
using MediatR;

namespace HRMS.Application.UseCases.Departments.Queries.GetDepartment
{
    public record GetDepartmentQuery(Guid departmentId) : IRequest<DepartmentDto>;

    public class GetDepartmentQueryHandler : IRequestHandler<GetDepartmentQuery, DepartmentDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetDepartmentQueryHandler(
            IApplicationDbContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<DepartmentDto> Handle(GetDepartmentQuery request, CancellationToken cancellationToken)
        {
            Department maybeDepartment = await _context.Departments
                .FindAsync(new object[] { request.departmentId });

            ValidateDepartmentIsNotNull(request, maybeDepartment);

            return _mapper.Map<DepartmentDto>(maybeDepartment);
        }

        private static void ValidateDepartmentIsNotNull(GetDepartmentQuery request, Department maybeDepartment)
        {
            if (maybeDepartment == null)
            {
                throw new NotFoundException(nameof(Department), request.departmentId);
            }
        }
    }
}
