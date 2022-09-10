using Application.Todos.Queries;
using Application.Todos.Commands;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.Dtos.Todos;
using API.ResponseHandlers;

namespace API.Controllers
{
    public class TodosController : BaseApiController
    {
        public TodosController(IMediator mediator, ICommandResponseHandler handler) : base(mediator, handler)
        {
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Todo>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Todo>>> GetTodosAsync()
        {
            return Ok(await Mediator.Send(new GetTodos.Query()).ConfigureAwait(false));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Todo), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Todo>> GetTodoAsync(Guid id)
        {
            var todo = await Mediator.Send(new GetTodo.Query(id)).ConfigureAwait(false);

            if (todo == null)
            {
                return NotFound(todo);
            }

            return Ok(todo);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateTodoAsync(CreateTodoDto createTodoDto)
        {
            var response = await Mediator.Send(new CreateTodo.Command(createTodoDto)).ConfigureAwait(false);

            return CommandResponseHandler.Handle(response);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> EditTodoAsync(EditTodoDto editTodoDto)
        {
            var response = await Mediator.Send(new EditTodo.Command(editTodoDto)).ConfigureAwait(false);

            return CommandResponseHandler.Handle(response);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteTodoAsync(Guid id)
        {
            var response = await Mediator.Send(new DeleteTodo.Command(id)).ConfigureAwait(false);

            return CommandResponseHandler.Handle(response);
        }
    }
}
