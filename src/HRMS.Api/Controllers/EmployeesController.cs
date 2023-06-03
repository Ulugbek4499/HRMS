using HRMS.Application.UseCases.Employees.Commands.CreateEmployee;
using HRMS.Application.UseCases.Employees.Commands.DeleteEmployee;
using HRMS.Application.UseCases.Employees.Commands.UpdateEmployee;
using HRMS.Application.UseCases.Employees.Models;
using HRMS.Application.UseCases.Employees.Queries.GetEmployee;
using HRMS.Application.UseCases.Employees.Queries.GetEmployees;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ApiControllerBase
    {
        [HttpPost("[action]")]
        public async ValueTask<ActionResult<EmployeeDto>> PostEmployeeAsync(CreateEmployeeCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpGet("[action]")]
        public async ValueTask<ActionResult<EmployeeDto>> GetEmployeeAsync(Guid EmployeeId)
        {
            return await Mediator.Send(new GetEmployeeQuery(EmployeeId));
        }

        [HttpGet("[action]")]
        public async ValueTask<ActionResult<EmployeeDto[]>> GetAllEmployee()
        {
            return await Mediator.Send(new GetEmployeesQuery());
        }

        [HttpPut("[action]")]
        public async ValueTask<ActionResult<EmployeeDto>> UpdateEmployeeAsync(Guid EmployeeId, UpdateEmployeeCommand command)
        {
            if (EmployeeId == null)
            {
                return BadRequest();
            }

            return await Mediator.Send(command);
        }

        [HttpDelete("[action]")]
        public async ValueTask<ActionResult<EmployeeDto>> DeleteEmployeeAsync(Guid EmployeeId)
        {
            return await Mediator.Send(new DeleteEmployeeCommand(EmployeeId));
        }
    }
}
