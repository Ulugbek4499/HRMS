﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using HRMS.Application.Common.Interfaces;
using HRMS.Application.UseCases.Positions.Models;
using HRMS.Application.UseCases.SalaryInfo.Models;
using HRMS.Domain.Entities.Employees;
using MediatR;

namespace HRMS.Application.UseCases.SalaryInfo.Query
{
    public record GetSalaryInfoQuery : IRequest<SalaryInfoDto[]>;

    public class GetSalaryInfoQueryHandler : IRequestHandler<GetSalaryInfoQuery, SalaryInfoDto[]>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetSalaryInfoQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<SalaryInfoDto[]> Handle(GetSalaryInfoQuery request, CancellationToken cancellationToken)
        {
            List<Employee> employees = _context.Employees.ToList();
            List<SalaryInfoDto> salaryInfoDtos= new List<SalaryInfoDto>();

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
                    ActualSalary= empl.Position.Salary* (decimal)TotalTime / empl.Position.MonthlyWorkingHours,
                });
            }


            return salaryInfoDtos.ToArray();
        }
    }
}