using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiControllerBase : ControllerBase
    {
        private IMediator? _mediator;
        public IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<IMediator>();
        protected IWebHostEnvironment _hostEnviroment
         => HttpContext.RequestServices.GetRequiredService<IWebHostEnvironment>();
    }
}
