using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRMS.Domain.Common;
using HRMS.Domain.Entities.Departments;
using HRMS.Domain.Entities.Employees;
using HRMS.Domain.Entities.Salaries;

namespace HRMS.Domain.Entities.Positions
{
    public class Position: BaseAuditableEntity
    {
        public string Name { get; set; }

        public Guid SalaryId { get; set; } 
        public Salary Salary { get; set; }

        public Guid DepartmentId { get; set; }
        public Department Department { get; set; }

        public ICollection<Employee> Employees { get; set; }
    }
}
