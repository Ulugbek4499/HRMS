using HRMS.Domain.Common;
using HRMS.Domain.Entities.Positions;
using HRMS.Domain.Entities.TimeSheets;

namespace HRMS.Domain.Entities.Employees
{
    public class Employee : BaseAuditableEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Guid PositionId { get; set; }
        public Position Position { get; set; }

        public ICollection<TimeSheet> TimeSheets { get; set; }
    }
}
