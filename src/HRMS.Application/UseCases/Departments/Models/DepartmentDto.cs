using System.Text.Json.Serialization;
using HRMS.Application.UseCases.Positions.Models;

namespace HRMS.Application.UseCases.Departments.Models
{
    public class DepartmentDto
    {
        [JsonPropertyName("department_id")]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<PositionDto> Positions { get; set; }
    }
}
