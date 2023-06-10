using HRMS.Api.Filters;
using HRMS.Application.UseCases.TimeSheets.Commands.CreateTimeSheet;
using HRMS.Application.UseCases.TimeSheets.Commands.DeleteTimeSheet;
using HRMS.Application.UseCases.TimeSheets.Commands.UpdateTimeSheet;
using HRMS.Application.UseCases.TimeSheets.Models;
using HRMS.Application.UseCases.TimeSheets.Queries.GetTimeSheet;
using HRMS.Application.UseCases.TimeSheets.Queries.GetTimeSheets;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimeSheetsController : ApiControllerBase
    {
        [RemoveLazyCache]
        [HttpPost("[action]")]
        public async ValueTask<ActionResult<TimeSheetDto>> PostTimeSheetAsync(CreateTimeSheetCommand command)
        {
            return await Mediator.Send(command);
        }


        [HttpGet("[action]")]
        public async ValueTask<ActionResult<TimeSheetDto>> GetTimeSheetAsync(Guid TimeSheetId)
        {
            return await Mediator.Send(new GetTimeSheetQuery(TimeSheetId));
        }

        [AddLazyCache]
        [HttpGet("[action]")]
        public async ValueTask<ActionResult<TimeSheetDto[]>> GetAllTimeSheet()
        {
            return await Mediator.Send(new GetTimeSheetsQuery());
        }

        [RemoveLazyCache]
        [HttpPost("updateTimeSheet")]
        public async ValueTask<ActionResult<TimeSheetDto>> UpdateTimeSheetAsync([FromForm] UpdateTimeSheetCommand command)
        {
            return await Mediator.Send(command);
        }

        [RemoveLazyCache]
        [HttpDelete("[action]")]
        public async ValueTask<ActionResult<TimeSheetDto>> DeleteTimeSheetAsync(Guid TimeSheetId)
        {
            return await Mediator.Send(new DeleteTimeSheetCommand(TimeSheetId));
        }
    }
}
