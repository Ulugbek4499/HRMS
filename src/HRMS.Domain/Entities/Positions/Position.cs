using HRMS.Domain.Common;
using HRMS.Domain.Entities.Departments;
using HRMS.Domain.Entities.Employees;

namespace HRMS.Domain.Entities.Positions
{
    public class Position : BaseAuditableEntity
    {
        public string Name { get; set; }

        public decimal Salary { get; set; }
        public int MonthlyWorkingHours { get; set; }

        public Guid DepartmentId { get; set; }
        public Department Department { get; set; }

        public ICollection<Employee> Employees { get; set; }
    }
}
