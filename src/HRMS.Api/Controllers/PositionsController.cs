using HRMS.Api.Filters;
using HRMS.Application.UseCases.Positions.Commands.CreatePosition;
using HRMS.Application.UseCases.Positions.Commands.DeletePosition;
using HRMS.Application.UseCases.Positions.Commands.UpdatePosition;
using HRMS.Application.UseCases.Positions.Models;
using HRMS.Application.UseCases.Positions.Queries.GetPosition;
using HRMS.Application.UseCases.Positions.Queries.GetPositions;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PositionsController : ApiControllerBase
    {
        [RemoveLazyCache]
        [HttpPost("[action]")]
        public async ValueTask<ActionResult<PositionDto>> PostPositionAsync(CreatePositionCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpGet("[action]")]
        public async ValueTask<ActionResult<PositionDto>> GetPositionAsync(Guid PositionId)
        {
            return await Mediator.Send(new GetPositionQuery(PositionId));
        }

        [AddLazyCache]
        [HttpGet("[action]")]
        public async ValueTask<ActionResult<PositionDto[]>> GetAllPosition()
        {
            return await Mediator.Send(new GetPositionsQuery());
        }

        [RemoveLazyCache]
        [HttpPost("updatePosition")]
        public async ValueTask<ActionResult<PositionDto>> UpdatePositionAsync([FromForm] UpdatePositionCommand command)
        {
           
            return await Mediator.Send(command);
        }

        [RemoveLazyCache]
        [HttpDelete("[action]")]
        public async ValueTask<ActionResult<PositionDto>> DeletePositionAsync(Guid PositionId)
        {
            return await Mediator.Send(new DeletePositionCommand(PositionId));
        }
    }
}

