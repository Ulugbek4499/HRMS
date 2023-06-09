﻿using HRMS.Application.Common.Models;
using HRMS.Application.UseCases.Employees.Commands.CreateEmployee;
using HRMS.Application.UseCases.Employees.Commands.DeleteEmployee;
using HRMS.Application.UseCases.Employees.Commands.UpdateEmployee;
using HRMS.Application.UseCases.Employees.Models;
using HRMS.Application.UseCases.Employees.Queries.GetEmployee;
using HRMS.Application.UseCases.Employees.Queries.GetEmployees;
using HRMS.Application.UseCases.Employees.Queries.GetEmployeesWithPagination;
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

        [HttpGet("[action]")]
        public async ValueTask<ActionResult<PaginatedList<EmployeeDto>>> GetEmployeesWithPagination(
            [FromQuery] GetEmployeesWithPaginationQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpPost("updateEmployee")]
        public async ValueTask<ActionResult<EmployeeDto>> UpdateEmployeeAsync([FromForm] UpdateEmployeeCommand command)
        {
            if ((await Mediator.Send(command)) is not null)
            {
                return NoContent();
            }
            return NoContent();
        }

        [HttpDelete("[action]")]
        public async ValueTask<ActionResult<EmployeeDto>> DeleteEmployeeAsync(Guid EmployeeId)
        {
            return await Mediator.Send(new DeleteEmployeeCommand(EmployeeId));
        }
    }
}

