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
        [HttpPost]
        public async ValueTask<ActionResult<TimeSheetDto>> PostTimeSheetAsync(CreateTimeSheetCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpGet]
        public async ValueTask<ActionResult<TimeSheetDto>> GetTimeSheetAsync(Guid TimeSheetId)
        {
            return await Mediator.Send(new GetTimeSheetQuery(TimeSheetId));
        }

        [HttpGet]
        public async ValueTask<ActionResult<TimeSheetDto[]>> GetAllTimeSheet()
        {
            return await Mediator.Send(new GetTimeSheetsQuery());
        }

        [HttpPut]
        public async ValueTask<ActionResult<TimeSheetDto>> UpdateTimeSheetAsync(Guid TimeSheetId, UpdateTimeSheetCommand command)
        {
            if (TimeSheetId == null)
            {
                return BadRequest();
            }

            return await Mediator.Send(command);
        }

        [HttpDelete]
        public async ValueTask<ActionResult<TimeSheetDto>> DeleteTimeSheetAsync(Guid TimeSheetId)
        {
            return await Mediator.Send(new DeleteTimeSheetCommand(TimeSheetId));
        }
    }
}
