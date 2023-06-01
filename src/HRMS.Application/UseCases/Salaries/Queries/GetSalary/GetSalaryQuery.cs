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

namespace HRMS.Application.UseCases.Salaries.Queries.GetSalary
{
    public record GetSalaryQuery(Guid SalaryId) : IRequest<SalaryDto> { }
    public class GetSalaryQueryHandler : IRequestHandler<GetSalaryQuery, SalaryDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public GetSalaryQueryHandler(
           IApplicationDbContext context,
           IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        public async Task<SalaryDto> Handle(GetSalaryQuery request, CancellationToken cancellationToken)
        {
            Salary maybeSalary = await _context.Salaries
             .FindAsync(new object[] { request.SalaryId });

            ValidateSalaryIsNotNull(request, maybeSalary);

            return _mapper.Map<SalaryDto>(maybeSalary);
        }

        private void ValidateSalaryIsNotNull(GetSalaryQuery request, Salary? maybeSalary)
        {
            if (maybeSalary == null)
            {
                throw new NotFoundException(nameof(Salary), request.SalaryId);
            }
        }
    }
}
