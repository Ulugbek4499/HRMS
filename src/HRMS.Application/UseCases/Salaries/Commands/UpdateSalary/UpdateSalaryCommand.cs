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

namespace HRMS.Application.UseCases.Salaries.Commands.UpdateSalary
{
    public class UpdateSalaryCommand:IRequest<SalaryDto>
    {
        public Guid Id { get; set; }
        public decimal SalaryAmount { get; set; }
        public int MonthlyWorkingHours { get; set; }
        public Guid PositionId { get; set; }
    }

    public class UpdateSalaryCommandHandler : IRequestHandler<UpdateSalaryCommand, SalaryDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public UpdateSalaryCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        public async Task<SalaryDto> Handle(UpdateSalaryCommand request, CancellationToken cancellationToken)
        {
            Salary maybeSalary = await
                 _context.Salaries.FindAsync(new object[] { request.Id });

            ValidateSalaryIsNotNull(request, maybeSalary);

            Position maybePosition = _context.Positions
                .SingleOrDefault(p => p.Id.Equals(request.PositionId));

            ValidatePositionsAreNotNull(request, maybePosition);

            maybeSalary.SalaryAmount = request.SalaryAmount;
            maybeSalary.MonthlyWorkingHours = request.MonthlyWorkingHours;
            maybeSalary.Position = maybePosition;

            await _context.SaveChangesAsync(cancellationToken);

            return _mapper.Map<SalaryDto>(maybePosition);
        }

        private void ValidatePositionsAreNotNull(UpdateSalaryCommand request, Position? maybePosition)
        {
            if (maybePosition is null)
            {
                throw new NotFoundException(nameof(Position), request.PositionId);
            }
        }

        private void ValidateSalaryIsNotNull(UpdateSalaryCommand request, Salary maybeSalary)
        {
            if (maybeSalary == null)
            {
                throw new NotFoundException(nameof(Salary), request.Id);
            }
        }
    }
}
