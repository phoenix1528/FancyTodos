using Application.Todos.Queries;
using Application.Todos.Commands;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.Dtos.Todos;

namespace API.Controllers
{
    public class TodosController : BaseApiController
    {
        public TodosController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Todo>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Todo>>> GetTodosAsync()
        {
            return Ok(await Mediator.Send(new GetTodos.Query()));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Todo), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Todo>> GetTodoAsync(Guid id)
        {
            var todo = await Mediator.Send(new GetTodo.Query(id));

            return ReturnCorrectStatusCode(todo);
        }

        [HttpPost]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateTodoAsync(CreateTodoDto createTodoDto)
        {
            return Ok(await Mediator.Send(new CreateTodo.Command(createTodoDto)));
        }
    }
}
