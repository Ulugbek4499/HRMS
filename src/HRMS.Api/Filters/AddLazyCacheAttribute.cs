using LazyCache;
using Microsoft.AspNetCore.Mvc.Filters;

namespace HRMS.Api.Filters
{
    public class AddLazyCacheAttribute : ActionFilterAttribute
    {
        private static IAppCache? _cache;
        private static IConfiguration? _configuration;
        private static string? key;
        private static TimeSpan duration;

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            _cache = context.HttpContext.RequestServices.GetRequiredService<IAppCache>();
            _configuration = context.HttpContext.RequestServices.GetRequiredService<IConfiguration>();


            if (context.HttpContext.Request.Path == "/api/Departments/GetAllDepartment")
                key = _configuration.GetValue<string>("LazyCache:DepartmentKey");

            if (context.HttpContext.Request.Path == "/api/Employees/GetAllEmployee")
                key = _configuration.GetValue<string>("LazyCache:EmployeeKey");

            if (context.HttpContext.Request.Path == "/api/Positions/GetAllPosition")
                key = _configuration.GetValue<string>("LazyCache:PositionKey");

            if (context.HttpContext.Request.Path == "/api/TimeSheets/GetAllTimeSheet")
                key = _configuration.GetValue<string>("LazyCache:TimeSheetKey");

            duration = TimeSpan.FromSeconds(_configuration.GetValue<double>("LazyCache:Duration"));
            var cachedResult = await _cache.GetOrAddAsync(key, () => next(), duration);

            if (cachedResult is not null)
                context.Result = cachedResult.Result;
        }
    }
}
