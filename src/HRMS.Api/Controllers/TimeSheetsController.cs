﻿using HRMS.Api.Filters;
using HRMS.Application.Common.Models;
using HRMS.Application.UseCases.TimeSheets.Commands.CreateTimeSheet;
using HRMS.Application.UseCases.TimeSheets.Commands.DeleteTimeSheet;
using HRMS.Application.UseCases.TimeSheets.Commands.UpdateTimeSheet;
using HRMS.Application.UseCases.TimeSheets.Models;
using HRMS.Application.UseCases.TimeSheets.Queries.GetTimeSheet;
using HRMS.Application.UseCases.TimeSheets.Queries.GetTimeSheets;
using HRMS.Application.UseCases.TimeSheets.Queries.GetTimeSheetsWithPagination;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimeSheetsController : ApiControllerBase
    {
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

        [HttpGet("[action]")]
        public async ValueTask<ActionResult<TimeSheetDto[]>> GetAllTimeSheet()
        {
            return await Mediator.Send(new GetTimeSheetsQuery());
        }

        [HttpGet("[action]")]
        public async ValueTask<PaginatedList<TimeSheetDto>> GetTimeSheetsWithPagination(
            [FromQuery] GetTimeSheetsWithPaginationQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpPost("updateTimeSheet")]
        public async ValueTask<ActionResult<TimeSheetDto>> UpdateTimeSheetAsync([FromForm] UpdateTimeSheetCommand command)
        {
            if ((await Mediator.Send(command)) is not null)
            {
                return NoContent();
            }
            return NoContent();
        }

        [HttpDelete("[action]")]
        public async ValueTask<ActionResult<TimeSheetDto>> DeleteTimeSheetAsync(Guid TimeSheetId)
        {
            return await Mediator.Send(new DeleteTimeSheetCommand(TimeSheetId));
        }
    }
}
