using LazyCache;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;

namespace HRMS.Api.Controllers
{
    [Route("api/[controller]")]
  //   [ResponseCache(Duration = 30)]
  //  [OutputCache(Duration =30)]
    [ApiController]
    public class ApiControllerBase : ControllerBase
    {
        private IMediator? _mediator;
        public IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<IMediator>();
    }
}
