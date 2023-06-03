using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRMS.Application.Common.Interfaces;
using HRMS.Infrastructure.Persistence.Interceptors;
using HRMS.Infrastructure.Persistence;
using HRMS.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HRMS.Infrastructure
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddInfrastructureService
            (this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<IApplicationDbContext, ApplicationDbContext>(options =>
            {
                options.UseNpgsql(connectionString: configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddScoped<AuditableEntitySaveChangesInterceptor>();
            services.AddScoped<IApplicationDbContext, ApplicationDbContext>();

            services.AddTransient<IDateTime, DateTimeService>();
            services.AddTransient<IGuidGenerator, GuidGeneratorService>();

            return services;
        }
    }
}
