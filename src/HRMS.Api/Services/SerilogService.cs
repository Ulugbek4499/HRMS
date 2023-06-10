using Serilog.Events;
using Serilog;

namespace HRMS.Api.Services
{
    public class SerilogService
    {
        public static void SerilogSettings(IConfiguration configuration)
        {
            Log.Logger = new LoggerConfiguration()
                             .Enrich.FromLogContext()
                             .ReadFrom.Configuration(configuration)
                             .CreateLogger();
        }
    }
}
