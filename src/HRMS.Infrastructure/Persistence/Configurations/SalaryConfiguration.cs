using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRMS.Domain.Entities.Salaries;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRMS.Infrastructure.Persistence.Configurations
{
    public class SalaryConfiguration : IEntityTypeConfiguration<Salary>
    {
        public void Configure(EntityTypeBuilder<Salary> builder)
        {
            throw new NotImplementedException();
        }
    }
}
