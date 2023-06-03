using HRMS.Application.UseCases.Positions.Commands.CreatePosition;
using HRMS.Application.UseCases.Positions.Commands.DeletePosition;
using HRMS.Application.UseCases.Positions.Commands.UpdatePosition;
using HRMS.Application.UseCases.Positions.Models;
using HRMS.Application.UseCases.Positions.Queries.GetPosition;
using HRMS.Application.UseCases.Positions.Queries.GetPositions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PositionsController : ApiControllerBase
    {
        [HttpPost]
        public async ValueTask<ActionResult<PositionDto>> PostPositionAsync(CreatePositionCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpGet]
        public async ValueTask<ActionResult<PositionDto>> GetPositionAsync(Guid PositionId)
        {
            return await Mediator.Send(new GetPositionQuery(PositionId));
        }

        [HttpGet]
        public async ValueTask<ActionResult<PositionDto[]>> GetAllPosition()
        {
            return await Mediator.Send(new GetPositionsQuery());
        }

        [HttpPut]
        public async ValueTask<ActionResult<PositionDto>> UpdatePositionAsync(Guid PositionId, UpdatePositionCommand command)
        {
            if (PositionId == null)
            {
                return BadRequest();
            }

            return await Mediator.Send(command);
        }

        [HttpDelete]
        public async ValueTask<ActionResult<PositionDto>> DeletePositionAsync(Guid PositionId)
        {
            return await Mediator.Send(new DeletePositionCommand(PositionId));
        }
    }
}
