using HRMS.Application.Common.Interfaces;
using HRMS.Domain.Entities.Departments;
using HRMS.Domain.Entities.Employees;
using HRMS.Domain.Entities.Positions;
using HRMS.Domain.Entities.Salaries;
using HRMS.Domain.Entities.TimeSheets;
using HRMS.Infrastructure.Persistence.Interceptors;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        private readonly DbContextOptions<ApplicationDbContext> _options;
        private readonly AuditableEntitySaveChangesInterceptor _interceptor;

        public ApplicationDbContext
            (DbContextOptions<ApplicationDbContext> options, 
            AuditableEntitySaveChangesInterceptor interceptor)
        {
            _options = options;
            _interceptor = interceptor;
        }

        public DbSet<Department> Departments {get; set; }
        public DbSet<Employee> Employees {get; set; }
        public DbSet<Position> Positions {get; set; }
        public DbSet<Salary> Salaries {get; set; }
        public DbSet<TimeSheet> TimeSheets {get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.AddInterceptors(_interceptor);
        }
    }
}
