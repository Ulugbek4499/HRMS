using AutoMapper;
using HRMS.Application.Common.Exceptions;
using HRMS.Application.Common.Interfaces;
using HRMS.Application.UseCases.Departments.Models;
using HRMS.Domain.Entities.Departments;
using MediatR;

namespace HRMS.Application.UseCases.Departments.Commands.CreateDepartment
{
    public class CreateDepartmentCommand:IRequest<DepartmentDto>
    {
        public string Name { get; set; }
    }

    public class CreateDepartmentCommandHandler : IRequestHandler<CreateDepartmentCommand, DepartmentDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CreateDepartmentCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<DepartmentDto> Handle(CreateDepartmentCommand request, CancellationToken cancellationToken)
        {
            Department maybeDepartment =
                _context.Departments.SingleOrDefault(d => d.Name.Equals(request.Name));

            ValidateDepartmentIsNull(request, maybeDepartment);

            var department=new Department()
            {
                Name=request.Name,
            };

            maybeDepartment = _context.Departments.Add(department).Entity;

            await _context.SaveChangesAsync(cancellationToken);

            return _mapper.Map<DepartmentDto>(maybeDepartment);
        }

        private static void ValidateDepartmentIsNull(CreateDepartmentCommand request, Department? maybeDepartment)
        {
            if (maybeDepartment != null)
            {
                throw new AlreadyExistsException(nameof(Department), request.Name);
            }
        }
    }
}
