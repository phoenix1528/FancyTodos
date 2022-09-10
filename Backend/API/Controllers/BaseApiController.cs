using API.ResponseHandlers;
using Application.Todos.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BaseApiController : ControllerBase
    {
        protected IMediator Mediator { get; private set; }
        protected ICommandResponseHandler CommandResponseHandler { get; private set; }

        public BaseApiController(IMediator mediator, ICommandResponseHandler handler)
        {
            Mediator = mediator;
            CommandResponseHandler = handler;
        }
    }
}
