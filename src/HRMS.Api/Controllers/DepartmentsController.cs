using HRMS.Api.Filters;
using HRMS.Application.Common.Models;
using HRMS.Application.UseCases.Departments.Commands.CreateDepartment;
using HRMS.Application.UseCases.Departments.Commands.DelateDepartment;
using HRMS.Application.UseCases.Departments.Commands.UpdateDepartment;
using HRMS.Application.UseCases.Departments.Models;
using HRMS.Application.UseCases.Departments.Queries.GetDepartment;
using HRMS.Application.UseCases.Departments.Queries.GetDepartments;
using HRMS.Application.UseCases.Departments.Queries.GetDepartmentsWithPagination;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace HRMS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ApiControllerBase
    {
        [HttpPost("[action]")]
        public async ValueTask<ActionResult<DepartmentDto>> PostDepartmentAsync(CreateDepartmentCommand command)
        {
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
           return await Mediator.Send(command);
        }


        [HttpDelete("[action]")]
        public async ValueTask<ActionResult<DepartmentDto>> DeleteDepartmentAsync(Guid departmentId)
        {
            return await Mediator.Send(new DeleteDepartmentCommand(departmentId));
        }
    }
}
