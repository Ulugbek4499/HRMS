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
using HRMS.Domain.Entities.Salaries;
using HRMS.Domain.Entities.TimeSheets;
using MediatR;

namespace HRMS.Application.UseCases.Salaries.Commands.DeleteSalary
{
    public record DeleteSalaryCommand(Guid salaryId): IRequest<SalaryDto>;

    public class DeleteSalaryCommandHandler : IRequestHandler<DeleteSalaryCommand, SalaryDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public DeleteSalaryCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<SalaryDto> Handle(DeleteSalaryCommand request, CancellationToken cancellationToken)
        {
            Salary maybeSalary = await
                   _context.Salaries.FindAsync(new object[] { request.salaryId });

            ValidateSalaryIsNotNull(request, maybeSalary);

            _context.Salaries.Remove(maybeSalary);

            await _context.SaveChangesAsync(cancellationToken);

            return _mapper.Map<SalaryDto>(maybeSalary);
        }

        private void ValidateSalaryIsNotNull(DeleteSalaryCommand request, Salary? maybeSalary)
        {
            if (maybeSalary is null)
            {
                throw new NotFoundException(nameof(Salary), request.salaryId);
            }
        }
    }
}
