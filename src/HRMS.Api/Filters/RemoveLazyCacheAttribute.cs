using HRMS.Domain.Entities.TimeSheets;
using LazyCache;
using Microsoft.AspNetCore.Mvc.Filters;

namespace HRMS.Api.Filters
{
    public class RemoveLazyCacheAttribute : ActionFilterAttribute
    {
        private static IAppCache? _cache;
        private static IConfiguration? _configuration;
        private static string? key;
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            _configuration = context.HttpContext.RequestServices.GetRequiredService<IConfiguration>();
            _cache = context.HttpContext.RequestServices.GetRequiredService<IAppCache>();


            if (context.HttpContext.Request.Path == "/api/Departments/PostDepartment")
                key = _configuration.GetValue<string>("LazyCache:DepartmentKey");

            if (context.HttpContext.Request.Path == "/api/Departments/UpdateDepartment")
                key = _configuration.GetValue<string>("LazyCache:DepartmentKey");

            if (context.HttpContext.Request.Path == "/api/Departments/DeleteDepartment")
                key = _configuration.GetValue<string>("LazyCache:DepartmentKey");


            if (context.HttpContext.Request.Path == "/api/Employees/PostEmployee")
                key = _configuration.GetValue<string>("LazyCache:EmployeeKey");

            if (context.HttpContext.Request.Path == "/api/Employees/updateEmployee")
                key = _configuration.GetValue<string>("LazyCache:EmployeeKey");

            if (context.HttpContext.Request.Path == "/api/Employees/DeleteEmployee")
                key = _configuration.GetValue<string>("LazyCache:EmployeeKey");


            if (context.HttpContext.Request.Path == "/api/Positions/PostPosition")
                key = _configuration.GetValue<string>("LazyCache:PositionKey");

            if (context.HttpContext.Request.Path == "/api/Positions/updatePosition")
                key = _configuration.GetValue<string>("LazyCache:PositionKey");

            if (context.HttpContext.Request.Path == "/api/Positions/DeletePosition")
                key = _configuration.GetValue<string>("LazyCache:PositionKey");


            if (context.HttpContext.Request.Path == "/api/TimeSheets/PostTimeSheet")
                key = _configuration.GetValue<string>("LazyCache:TimeSheetKey");

            if (context.HttpContext.Request.Path == "/api/TimeSheets/updateTimeSheet")
                key = _configuration.GetValue<string>("LazyCache:TimeSheetKey");

            if (context.HttpContext.Request.Path == "/api/TimeSheets/DeleteTimeSheet")
                key = _configuration.GetValue<string>("LazyCache:TimeSheetKey");

            _cache.Remove(key);
            await next();
        }
    }
}
