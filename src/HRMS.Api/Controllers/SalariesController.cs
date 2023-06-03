using HRMS.Application.UseCases.Salaries.Commands.CreateSalary;
using HRMS.Application.UseCases.Salaries.Commands.DeleteSalary;
using HRMS.Application.UseCases.Salaries.Commands.UpdateSalary;
using HRMS.Application.UseCases.Salaries.Models;
using HRMS.Application.UseCases.Salaries.Queries.GetSalaries;
using HRMS.Application.UseCases.Salaries.Queries.GetSalary;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalariesController : ApiControllerBase
    {
        [HttpPost]
        public async ValueTask<ActionResult<SalaryDto>> PostSalaryAsync(CreateSalaryCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpGet]
        public async ValueTask<ActionResult<SalaryDto>> GetSalaryAsync(Guid SalaryId)
        {
            return await Mediator.Send(new GetSalaryQuery(SalaryId));
        }

        [HttpGet]
        public async ValueTask<ActionResult<SalaryDto[]>> GetAllSalary()
        {
            return await Mediator.Send(new GetSalariesQuery());
        }

        [HttpPut]
        public async ValueTask<ActionResult<SalaryDto>> UpdateSalaryAsync(Guid SalaryId, UpdateSalaryCommand command)
        {
            if (SalaryId == null)
            {
                return BadRequest();
            }

            return await Mediator.Send(command);
        }

        [HttpDelete]
        public async ValueTask<ActionResult<SalaryDto>> DeleteSalaryAsync(Guid SalaryId)
        {
            return await Mediator.Send(new DeleteSalaryCommand(SalaryId));
        }
    }
}
