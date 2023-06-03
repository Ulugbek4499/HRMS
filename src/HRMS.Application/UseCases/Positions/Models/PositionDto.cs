using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using HRMS.Application.UseCases.Departments.Models;
using HRMS.Application.UseCases.Employees.Models;
using HRMS.Application.UseCases.Salaries.Models;
using HRMS.Domain.Entities.Departments;
using HRMS.Domain.Entities.Employees;
using HRMS.Domain.Entities.Salaries;

namespace HRMS.Application.UseCases.Positions.Models
{
    public class PositionDto
    {
        [JsonPropertyName("position_id")]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public SalaryDto Salary { get; set; }
        public DepartmentDto Department { get; set; }
        public ICollection<EmployeeDto> Employees { get; set; }
    }
}
