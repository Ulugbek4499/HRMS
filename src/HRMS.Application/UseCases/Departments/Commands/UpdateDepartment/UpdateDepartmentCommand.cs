using System.Data;
using AutoMapper;
using HRMS.Application.Common.Exceptions;
using HRMS.Application.Common.Interfaces;
using HRMS.Application.UseCases.Departments.Models;
using HRMS.Domain.Entities.Departments;
using HRMS.Domain.Entities.Positions;
using MediatR;

namespace HRMS.Application.UseCases.Departments.Commands.UpdateDepartment
{
    public class UpdateDepartmentCommand:IRequest<DepartmentDto>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<Guid> Positions { get; set; }
    }

    public class UpdateDepartmentCommandHandler : IRequestHandler<UpdateDepartmentCommand, DepartmentDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public UpdateDepartmentCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<DepartmentDto> Handle(UpdateDepartmentCommand request, CancellationToken cancellationToken)
        {
            Department maybeDepartment = await _context.Departments
                .FindAsync(new object[] { request.Id }, cancellationToken);

            ValidateDepartmentIsNotNull(request, maybeDepartment);


            bool areAllExist = request.Positions.All(
                x => _context.Positions.Any(p => p.Id.Equals(x)));

            ValidateAllPositionsExist(areAllExist);

            List<Position> positions = _context.Positions
                .Where(r => request.Positions.Contains(r.Id)).ToList();

            maybeDepartment.Name = request.Name;
            maybeDepartment.Positions = positions;

            maybeDepartment = _context.Departments.Update(maybeDepartment).Entity;

            await _context.SaveChangesAsync(cancellationToken);

            return _mapper.Map<DepartmentDto>(maybeDepartment);
        }

        private static void ValidateAllPositionsExist(bool areAllExist)
        {
            if (!areAllExist)
            {
                throw new NotFoundException("Position does not exist");
            }
        }

        private static void ValidateDepartmentIsNotNull(UpdateDepartmentCommand request, Department? maybeDepartment)
        {
            if (maybeDepartment is null)
            {
                throw new NotFoundException(nameof(Department), request.Id);
            }
        }

    }
}
