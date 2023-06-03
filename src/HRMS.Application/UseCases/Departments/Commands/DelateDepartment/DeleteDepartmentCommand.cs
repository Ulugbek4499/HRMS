using AutoMapper;
using HRMS.Application.Common.Exceptions;
using HRMS.Application.Common.Interfaces;
using HRMS.Application.UseCases.Departments.Models;
using HRMS.Domain.Entities.Departments;
using MediatR;

namespace HRMS.Application.UseCases.Departments.Commands.DelateDepartment
{
    public record DeleteDepartmentCommand(Guid departmentId) : IRequest<DepartmentDto>;

    public class DeleteDepartmentCommandHandler : IRequestHandler<DeleteDepartmentCommand, DepartmentDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public DeleteDepartmentCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<DepartmentDto> Handle(DeleteDepartmentCommand request, CancellationToken cancellationToken)
        {
            Department maybeDepartment = await
                _context.Departments.FindAsync(new object[] { request.departmentId });

            ValidateDepartmentIsNotNull(request, maybeDepartment);

            _context.Departments.Remove(maybeDepartment);

            await _context.SaveChangesAsync(cancellationToken);

            return _mapper.Map<DepartmentDto>(maybeDepartment);
        }

        private static void ValidateDepartmentIsNotNull(DeleteDepartmentCommand request, Department maybeDepartment)
        {
            if (maybeDepartment is null)
            {
                throw new NotFoundException(nameof(Department), request.departmentId);
            }
        }
    }
}
