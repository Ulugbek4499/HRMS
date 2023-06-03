using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRMS.Application.Common.Interfaces;

namespace HRMS.Infrastructure.Services
{
    public class GuidGeneratorService : IGuidGenerator
    {
        public Guid Guid => Guid.NewGuid();
    }
}
