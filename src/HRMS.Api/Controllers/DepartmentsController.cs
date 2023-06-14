using HRMS.Api.Filters;
using HRMS.Application.Common.Models;
using HRMS.Application.UseCases.Departments.Commands.CreateDepartment;
using HRMS.Application.UseCases.Departments.Commands.DelateDepartment;
using HRMS.Application.UseCases.Departments.Commands.UpdateDepartment;
using HRMS.Application.UseCases.Departments.Models;
using HRMS.Application.UseCases.Departments.Queries.GetDepartment;
using HRMS.Application.UseCases.Departments.Queries.GetDepartments;
using HRMS.Application.UseCases.Departments.Queries.GetDepartmentsWithPagination;
using LazyCache;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace HRMS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ApiControllerBase
    {

        private readonly IAppCache _lazyCache;

        private const string My_Key = "My_Key";

        public DepartmentsController(IAppCache lazyCache)
        {
            _lazyCache = lazyCache;
        }

        [HttpPost("[action]")]
        public async ValueTask<ActionResult<DepartmentDto>> PostDepartmentAsync(CreateDepartmentCommand command)
        {
            _lazyCache.Remove(My_Key);

            return await Mediator.Send(command);
        }

        [HttpGet("[action]")]
        public async ValueTask<ActionResult<DepartmentDto>> GetDepartmentAsync(Guid departmentId)
        {
            return await Mediator.Send(new GetDepartmentQuery(departmentId));
        }

        [LazyCache(10, 30)]
        [HttpGet("[action]")]
        [EnableRateLimiting("TokenBucket")]
        public async ValueTask<ActionResult<DepartmentDto[]>> GetAllDepartment()
        {
                return await Mediator.Send(new GetDepartmentsQuery());
        }

        [HttpGet("[action]")]
        public async ValueTask<ActionResult<PaginatedList<DepartmentDto>>> GetDepartmentsWithPagination([FromQuery] GetDepartmentsWithPaginationQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpPut("[action]")]
        public async ValueTask<ActionResult<DepartmentDto>> UpdateDepartmentAsync(UpdateDepartmentCommand command)
        {
            _lazyCache.Remove(My_Key);

            return await Mediator.Send(command);
        }


        [HttpDelete("[action]")]
        public async ValueTask<ActionResult<DepartmentDto>> DeleteDepartmentAsync(Guid departmentId)
        {
            _lazyCache.Remove(My_Key);

            return await Mediator.Send(new DeleteDepartmentCommand(departmentId));
        }
    }
}
