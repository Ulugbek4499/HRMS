using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using HRMS.Application.Common.Interfaces;
using HRMS.Application.UseCases.Salaries.Models;
using HRMS.Application.UseCases.TimeSheets.Models;
using HRMS.Application.UseCases.TimeSheets.Queries.GetTimeSheets;
using HRMS.Domain.Entities.Salaries;
using HRMS.Domain.Entities.TimeSheets;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Application.UseCases.Salaries.Queries.GetSalaries
{
    public record GetSalariesQuery : IRequest<SalaryDto[]>;

    public class GetSalariesQueryHandler : IRequestHandler<GetSalariesQuery, SalaryDto[]>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetSalariesQueryHandler(
            IApplicationDbContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<SalaryDto[]> Handle(GetSalariesQuery request, CancellationToken cancellationToken)
        {
            Salary[] salaries = await _context.Salaries.ToArrayAsync();

            return _mapper.Map<SalaryDto[]>(salaries);
        }
    }
}
