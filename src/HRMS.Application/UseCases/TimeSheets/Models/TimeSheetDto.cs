using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using HRMS.Application.UseCases.Employees.Models;
using HRMS.Domain.Entities.Employees;

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
