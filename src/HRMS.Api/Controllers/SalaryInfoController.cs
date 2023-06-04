using HRMS.Application.UseCases.Positions.Models;
using HRMS.Application.UseCases.Positions.Queries.GetPositions;
using HRMS.Application.UseCases.SalaryInfo.Models;
using HRMS.Application.UseCases.SalaryInfo.Query;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalaryInfoController : ApiControllerBase
    {

        [HttpGet("[action]")]
        public async ValueTask<ActionResult<SalaryInfoDto[]>> GetAllSalaryInfo()
        {
            return await Mediator.Send(new GetSalaryInfoQuery());
        }

    }
}
