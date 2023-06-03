using System.Text.Json.Serialization;
using HRMS.Application.UseCases.Employees.Models;

namespace HRMS.Application.UseCases.TimeSheets.Models
{
    public class TimeSheetDto
    {
        [JsonPropertyName("timeSheet_id")]
        public Guid Id { get; set; }
        public DateTimeOffset WorkingDay { get; set; }
        public double WorkedHours { get; set; }
        public EmployeeDto Employee { get; set; }
    }
}
