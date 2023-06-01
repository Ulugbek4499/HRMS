using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRMS.Domain.Common;
using HRMS.Domain.Entities.Positions;

namespace HRMS.Domain.Entities.Departments
{
    public class Department: BaseAuditableEntity
    {
        public string Name { get; set; }

        public ICollection<Position> Positions { get; set; }
    }
}
