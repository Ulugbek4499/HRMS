using HRMS.Application.UseCases.Positions.Models;
using Newtonsoft.Json;

namespace HRMS.Application.UseCases.Employees.Models
{
    public class EmployeeDto
    {
        [JsonProperty("employee_id")]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }

        public PositionDto Position { get; set; }
        //  public ICollection<TimeSheetDto> TimeSheets { get; set; }
    }
}
