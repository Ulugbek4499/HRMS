using AutoMapper;
using HRMS.Application.UseCases.Departments.Models;
using HRMS.Application.UseCases.Employees.Models;
using HRMS.Application.UseCases.Positions.Models;
using HRMS.Application.UseCases.TimeSheets.Models;
using HRMS.Domain.Entities.Departments;
using HRMS.Domain.Entities.Employees;
using HRMS.Domain.Entities.Positions;
using HRMS.Domain.Entities.TimeSheets;

namespace HRMS.Application.Common.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Department, DepartmentDto>().ReverseMap();
            CreateMap<Employee, EmployeeDto>().ReverseMap();
            CreateMap<Position, PositionDto>().ReverseMap();
            CreateMap<TimeSheet, TimeSheetDto>().ReverseMap();
        }
    }
}
