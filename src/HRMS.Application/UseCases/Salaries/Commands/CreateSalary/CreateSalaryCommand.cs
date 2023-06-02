using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using HRMS.Application.Common.Exceptions;
using HRMS.Application.Common.Interfaces;
using HRMS.Application.UseCases.Salaries.Models;
using HRMS.Application.UseCases.TimeSheets.Models;
using HRMS.Domain.Entities.Employees;
using HRMS.Domain.Entities.Positions;
using HRMS.Domain.Entities.Salaries;
using HRMS.Domain.Entities.TimeSheets;
using MediatR;

namespace HRMS.Application.UseCases.Salaries.Commands.CreateSalary
{
    public class CreateSalaryCommand:IRequest<SalaryDto>
    {
        public decimal SalaryAmount { get; set; }
        public int MonthlyWorkingHours { get; set; }
        public Guid PositionId { get; set; }
    }

    public class CreateSalaryCommandHandler : IRequestHandler<CreateSalaryCommand, SalaryDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CreateSalaryCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<SalaryDto> Handle(CreateSalaryCommand request, CancellationToken cancellationToken)
        {

            Position maybePosition =
                _context.Positions.SingleOrDefault(p => p.Id.Equals(request.PositionId));

            ValidatePositionAreNotNull(request, maybePosition);

            var salary = new Salary
            {
                SalaryAmount = request.SalaryAmount,
                MonthlyWorkingHours = request.MonthlyWorkingHours,
                Position = maybePosition
            };

            salary = _context.Salaries.Add(salary).Entity;

            await _context.SaveChangesAsync(cancellationToken);

            return _mapper.Map<SalaryDto>(salary);
        }

        private void ValidatePositionAreNotNull(CreateSalaryCommand request, Position? maybePosition)
        {
            if (maybePosition is null)
            {
                throw new NotFoundException(nameof(Position), request.PositionId);
            }
        }
    }
}
