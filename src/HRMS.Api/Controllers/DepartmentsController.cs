using HRMS.Application.UseCases.Departments.Commands.CreateDepartment;
using HRMS.Application.UseCases.Departments.Commands.DelateDepartment;
using HRMS.Application.UseCases.Departments.Commands.UpdateDepartment;
using HRMS.Application.UseCases.Departments.Models;
using HRMS.Application.UseCases.Departments.Queries.GetDepartment;
using HRMS.Application.UseCases.Departments.Queries.GetDepartments;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ApiControllerBase
    {
        [HttpPost]
        public async ValueTask<ActionResult<DepartmentDto>> PostDepartmentAsync(CreateDepartmentCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpGet]
        public async ValueTask<ActionResult<DepartmentDto>> GetDepartmentAsync(Guid departmentId)
        {
            return await Mediator.Send(new GetDepartmentQuery(departmentId));
        }

        [HttpGet]
        public async ValueTask<ActionResult<DepartmentDto[]>> GetAllDepartment()
        {
            return await Mediator.Send(new GetDepartmentsQuery());
        }

        [HttpPut]
        public async ValueTask<ActionResult<DepartmentDto>> UpdateDepartmentAsync(Guid departmentId, UpdateDepartmentCommand command)
        {
            if (departmentId == null)
            {
                return BadRequest();
            }

            return await Mediator.Send(command);
        }

        [HttpDelete]
        public async ValueTask<ActionResult<DepartmentDto>> DeleteDepartmentAsync(Guid departmentId)
        {
            return await Mediator.Send(new DeleteDepartmentCommand(departmentId));
        }
    }
}
