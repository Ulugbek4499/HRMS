using HRMS.Application.Common.Interfaces;

namespace HRMS.Infrastructure.Services
{
    public class GuidGeneratorService : IGuidGenerator
    {
        public Guid Guid => Guid.NewGuid();
    }
}
