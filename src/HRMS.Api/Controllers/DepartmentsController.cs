using HRMS.Application.UseCases.Departments.Commands.CreateDepartment;
using HRMS.Application.UseCases.Departments.Commands.DelateDepartment;
using HRMS.Application.UseCases.Departments.Commands.UpdateDepartment;
using HRMS.Application.UseCases.Departments.Models;
using HRMS.Application.UseCases.Departments.Queries.GetDepartment;
using HRMS.Application.UseCases.Departments.Queries.GetDepartments;
using LazyCache;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

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


        [HttpGet("[action]")]
        public async ValueTask<ActionResult<DepartmentDto[]>> GetAllDepartment()
        {
                return await Mediator.Send(new GetDepartmentsQuery());
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
