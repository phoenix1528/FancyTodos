using Application.Todos.Commands;
using Microsoft.AspNetCore.Mvc;

namespace API.ResponseHandlers
{
    public interface ICommandResponseHandler
    {
        ActionResult Handle(ICreateCommandResponse commandResponse);
        ActionResult Handle(IEditCommandResponse commandResponse);
        ActionResult Handle(IDeleteCommandResponse commandResponse);
    }
}
