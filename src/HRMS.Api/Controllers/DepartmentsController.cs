﻿using HRMS.Api.Filters;
using HRMS.Application.UseCases.Departments.Commands.CreateDepartment;
using HRMS.Application.UseCases.Departments.Commands.DelateDepartment;
using HRMS.Application.UseCases.Departments.Commands.UpdateDepartment;
using HRMS.Application.UseCases.Departments.Models;
using HRMS.Application.UseCases.Departments.Queries.GetDepartment;
using HRMS.Application.UseCases.Departments.Queries.GetDepartments;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace HRMS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ApiControllerBase
    {
        [RemoveLazyCache]
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

        [AddLazyCache]
        [HttpGet("[action]")]
        [EnableRateLimiting("TokenBucket")]
        public async ValueTask<ActionResult<DepartmentDto[]>> GetAllDepartment()
        {
                return await Mediator.Send(new GetDepartmentsQuery());
        }

        [RemoveLazyCache]
        [HttpPut("[action]")]
        public async ValueTask<ActionResult<DepartmentDto>> UpdateDepartmentAsync(UpdateDepartmentCommand command)
        {
            return await Mediator.Send(command);
        }

        [RemoveLazyCache]
        [HttpDelete("[action]")]
        public async ValueTask<ActionResult<DepartmentDto>> DeleteDepartmentAsync(Guid departmentId)
        {
            return await Mediator.Send(new DeleteDepartmentCommand(departmentId));
        }
    }
}
