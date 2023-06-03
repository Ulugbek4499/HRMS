using HRMS.Domain.Entities.Positions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRMS.Infrastructure.Persistence.Configurations
{
    public class PositionConfiguration : IEntityTypeConfiguration<Position>
    {
        public void Configure(EntityTypeBuilder<Position> builder)
        {
            builder.Navigation(p => p.Employees).AutoInclude();
            builder.Navigation(p => p.Department).AutoInclude();
        }
    }
}
