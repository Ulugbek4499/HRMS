using AutoMapper;
using HRMS.Application.Common.Interfaces;
using HRMS.Application.UseCases.SalaryInfo.Models;
using HRMS.Domain.Entities.Employees;
using MediatR;

namespace HRMS.Application.UseCases.SalaryInfo.Query
{
    public record GetSalaryInfoQuery : IRequest<SalaryInfoDto[]>;

    public class GetSalaryInfoQueryHandler : IRequestHandler<GetSalaryInfoQuery, SalaryInfoDto[]>
    {
        private readonly IApplicationDbContext _context;

        public GetSalaryInfoQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<SalaryInfoDto[]> Handle(GetSalaryInfoQuery request, CancellationToken cancellationToken)
        {
            List<Employee> employees = _context.Employees.ToList();
            List<SalaryInfoDto> salaryInfoDtos = new List<SalaryInfoDto>();

            foreach (Employee empl in employees)
            {
                double TotalTime = 0;
                var TotalHoursList = empl.TimeSheets.Where(x => x.EmployeeId == empl.Id).ToList();

                TotalHoursList.ForEach(x => TotalTime += x.WorkedHours);

                salaryInfoDtos.Add(new SalaryInfoDto()
                {
                    Name = empl.Name,
                    PositionName = empl.Position.Name,
                    FixedWorkingHours = empl.Position.MonthlyWorkingHours,
                    FixedSalary = empl.Position.Salary,
                    ActualWorkingHours = TotalTime,
                    ActualSalary = empl.Position.Salary * (decimal)TotalTime / empl.Position.MonthlyWorkingHours,
                });
            }


            return salaryInfoDtos.ToArray();
        }
    }
}
