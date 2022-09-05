using Application.Todos.Queries;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class TodosController : BaseApiController
    {
        public TodosController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet]
        public async Task<ActionResult<List<Todo>>> GetTodos()
        {
            return Ok(await Mediator.Send(new GetTodos.Query()));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Todo>> GetTodo(Guid id)
        {
            var todo = await Mediator.Send(new GetTodo.Query(id));

            return ReturnCorrectStatusCode(todo);
        }
    }
}
