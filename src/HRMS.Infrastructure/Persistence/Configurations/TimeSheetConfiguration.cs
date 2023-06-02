using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRMS.Domain.Entities.TimeSheets;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRMS.Infrastructure.Persistence.Configurations
{
    public class TimeSheetConfiguration : IEntityTypeConfiguration<TimeSheet>
    {
        public void Configure(EntityTypeBuilder<TimeSheet> builder)
        {
            throw new NotImplementedException();
        }
    }
}
