using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using HRMS.Application.Common.Interfaces;
using HRMS.Application.UseCases.Positions.Models;
using HRMS.Domain.Entities.Departments;
using HRMS.Domain.Entities.Employees;
using HRMS.Domain.Entities.Positions;
using HRMS.Domain.Entities.Salaries;
using MediatR;

namespace HRMS.Application.UseCases.Positions.Commands.CreatePosition
{
    public class CreatePositionCommand:IRequest<PositionDto>
    {
        public string Name { get; set; }
        public Guid SalaryId { get; set; }
        public Guid DepartmentId { get; set; }
    }

    public class CreatePositionCommandHanlder : IRequestHandler<CreatePositionCommand, PositionDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CreatePositionCommandHanlder(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PositionDto> Handle(CreatePositionCommand request, CancellationToken cancellationToken)
        {
            Salary maybeSalary = await
               _context.Salaries.FindAsync(new object[] { request.SalaryId });
            
            ValidateSalaryIsNotNull(request, maybeSalary);

            Department maybeDepartment=await
                _context.Departments.FindAsync(new object[] { request.DepartmentId });

            ValidateDepartmentIsNotNull(request, maybeDepartment);

            var position = new Position()
            {
                Name = request.Name,
                Salary = maybeSalary;
                De
            }


        }

        private void ValidateDepartmentIsNotNull(CreatePositionCommand request, Department? maybeDepartment)
        {
            throw new NotImplementedException();
        }

        private void ValidateSalaryIsNotNull(CreatePositionCommand request, Salary? maybeSalary)
        {
            throw new NotImplementedException();
        }
    }
}
