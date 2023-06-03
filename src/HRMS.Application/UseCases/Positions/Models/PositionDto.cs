using System.Text.Json.Serialization;
using HRMS.Application.UseCases.Departments.Models;
using HRMS.Application.UseCases.Employees.Models;

namespace HRMS.Application.UseCases.Positions.Models
{
    public class PositionDto
    {
        [JsonPropertyName("position_id")]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Salary { get; set; }
        public int MonthlyWorkingHours { get; set; }
      //  public DepartmentDto Department { get; set; }
        public ICollection<EmployeeDto> Employees { get; set; }
    }
}
