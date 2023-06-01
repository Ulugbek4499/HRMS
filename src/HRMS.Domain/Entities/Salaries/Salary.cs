using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRMS.Domain.Common;
using HRMS.Domain.Entities.Positions;

namespace HRMS.Domain.Entities.Salaries
{
    public class Salary: BaseAuditableEntity
    {
        public decimal SalaryAmount { get; set; }
        public int MonthlyWorkingHours { get; set; }

        public Guid PositionId { get; set; }
        public Position Position { get; set; }
    }
}
