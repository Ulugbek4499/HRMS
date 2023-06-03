using Microsoft.AspNetCore.Mvc;

namespace HRMS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalariesController : ApiControllerBase
    {
        [HttpPost("[action]")]
        public async ValueTask<ActionResult<SalaryDto>> PostSalaryAsync(CreateSalaryCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpGet("[action]")]
        public async ValueTask<ActionResult<SalaryDto>> GetSalaryAsync(Guid SalaryId)
        {
            return await Mediator.Send(new GetSalaryQuery(SalaryId));
        }

        [HttpGet("[action]")]
        public async ValueTask<ActionResult<SalaryDto[]>> GetAllSalary()
        {
            return await Mediator.Send(new GetSalariesQuery());
        }

        [HttpPut("[action]")]
        public async ValueTask<ActionResult<SalaryDto>> UpdateSalaryAsync(Guid SalaryId, UpdateSalaryCommand command)
        {
            if (SalaryId == null)
            {
                return BadRequest();
            }

            return await Mediator.Send(command);
        }

        [HttpDelete("[action]")]
        public async ValueTask<ActionResult<SalaryDto>> DeleteSalaryAsync(Guid SalaryId)
        {
            return await Mediator.Send(new DeleteSalaryCommand(SalaryId));
        }
    }
}
