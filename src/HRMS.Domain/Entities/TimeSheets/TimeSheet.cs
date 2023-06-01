using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRMS.Domain.Common;
using HRMS.Domain.Entities.Employees;

namespace HRMS.Domain.Entities.TimeSheets
{
    public class TimeSheet: BaseAuditableEntity
    {
        public DateTimeOffset WorkingDay { get; set; }
        public double WorkedHours { get; set; }
        public Guid EmployeeId { get; set; }
        public Employee Employee { get; set; }
    }

}
