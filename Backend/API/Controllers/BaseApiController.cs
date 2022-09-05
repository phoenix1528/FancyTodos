using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BaseApiController : ControllerBase
    {
        protected IMediator Mediator { get; private set; }

        public BaseApiController(IMediator mediator)
        {
            Mediator = mediator;
        }

        public ActionResult ReturnCorrectStatusCode<T>(T entity)
        {
            if (entity == null)
            {
                return NotFound(entity);
            }

            return Ok(entity);
        }
    }
}
