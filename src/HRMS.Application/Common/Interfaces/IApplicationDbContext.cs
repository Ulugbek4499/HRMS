using HRMS.Domain.Entities.Departments;
using HRMS.Domain.Entities.Employees;
using HRMS.Domain.Entities.Positions;
using HRMS.Domain.Entities.TimeSheets;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Salary> Salaries { get; set; }
        public DbSet<TimeSheet> TimeSheets { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        IQueryable<T> GetByIds<T>(IEnumerable<Guid> ids) where T : class;
    }
}
