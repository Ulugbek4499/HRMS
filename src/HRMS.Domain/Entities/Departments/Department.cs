using HRMS.Domain.Common;
using HRMS.Domain.Entities.Positions;

namespace HRMS.Domain.Entities.Departments
{
    public class Department : BaseAuditableEntity
    {
        public string Name { get; set; }

        public ICollection<Position> Positions { get; set; }
    }
}
