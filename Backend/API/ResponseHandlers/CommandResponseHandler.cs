using Application.Todos.Commands;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.ResponseCompression;
using System.Diagnostics;

namespace API.ResponseHandlers
{
    public class CommandResponseHandler : ICommandResponseHandler
    {
        public ActionResult Handle(ICreateCommandResponse commandResponse)
        {
            if (!commandResponse.ValidationSuccess)
            {
                return new BadRequestObjectResult(commandResponse.ValidationErrors);
            }

            return new CreatedResult("api/todos", commandResponse.ItemId);
        }

        public ActionResult Handle(IEditCommandResponse commandResponse)
        {
            if (!commandResponse.ItemExists)
            {
                return new NotFoundObjectResult(commandResponse.ItemId);
            }

            if (!commandResponse.ValidationSuccess)
            {
                return new BadRequestObjectResult(commandResponse.ValidationErrors);
            }

            return new NoContentResult();
        }

        public ActionResult Handle(IDeleteCommandResponse commandResponse)
        {
            if (!commandResponse.ItemExists)
            {
                return new NotFoundObjectResult(commandResponse.ItemId);
            }

            return new NoContentResult();
        }
    }
}
