using API.ResponseHandlers;
using Moq;
using MediatR;
using API.Controllers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Domain;
using Application.Todos.Commands;
using FluentValidation.Results;
using Application.Todos.Queries;

namespace Tests.Todos.Api
{
    public class TodosControllerTests
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly Mock<ICommandResponseHandler> _commandResponseHandlerMock;
        private readonly IEnumerable<Todo> Todos = TodosDataHelper.GenerateTodos();
        private readonly Todo Todo = TodosDataHelper.GenerateSingleTodo();

        public TodosControllerTests()
        {
            _mediatorMock = new Mock<IMediator>();
            _commandResponseHandlerMock = new Mock<ICommandResponseHandler>();
        }

        [Fact]
        [Trait("Category", "Unit")]
        public async Task GetTodos_WhenNoTodosExist_ReturnsEmptyListAndOkStatusCode()
        {
            _mediatorMock.Setup(x => x.Send(It.IsAny<GetTodos.Query>(), default))
                .ReturnsAsync(Enumerable.Empty<Todo>());

            var controller = new TodosController(_mediatorMock.Object, _commandResponseHandlerMock.Object);
            
            var result = await controller.GetTodosAsync();
            
            
            result.Result.Should().BeOfType<OkObjectResult>();

            var okResult = (OkObjectResult)result!.Result!;
            var todos = (IEnumerable<Todo>)okResult!.Value!;
            todos.Should().BeEmpty();
        }

        [Fact]
        [Trait("Category", "Unit")]
        public async Task GetTodos_WhenTodosExist_ReturnsAllTodosAndOkStatusCode()
        {
            _mediatorMock.Setup(x => x.Send(It.IsAny<GetTodos.Query>(), default))
                .ReturnsAsync(Todos);

            var controller = new TodosController(_mediatorMock.Object, _commandResponseHandlerMock.Object);

            var result = await controller.GetTodosAsync();

            result.Result.Should().BeOfType<OkObjectResult>();

            var okResult = (OkObjectResult)result!.Result!;
            var todos = (IEnumerable<Todo>)okResult!.Value!;
            todos.Should().HaveCount(Todos.Count());
        }


        [Fact]
        [Trait("Category", "Unit")]
        public async Task GetTodo_WhenTodoWasNotFound_ReturnsNullAndNotFoundStatusCode()
        {
            _mediatorMock.Setup(x => x.Send(It.IsAny<GetTodo.Query>(), default))
                .ReturnsAsync((Todo?)null);

            var controller = new TodosController(_mediatorMock.Object, _commandResponseHandlerMock.Object);

            var result = await controller.GetTodoAsync(Guid.NewGuid());

            result.Result.Should().BeOfType<NotFoundObjectResult>();

            var okResult = (NotFoundObjectResult)result!.Result!;
            var todo = (Todo)okResult!.Value!;
            todo.Should().BeNull();
        }

        [Fact]
        [Trait("Category", "Unit")]
        public async Task GetTodo_WhenTodoExists_ReturnsTodoAndOkStatusCode()
        {
            _mediatorMock.Setup(x => x.Send(It.IsAny<GetTodo.Query>(), default))
                .ReturnsAsync(Todo);

            var controller = new TodosController(_mediatorMock.Object, _commandResponseHandlerMock.Object);

            var result = await controller.GetTodoAsync(Todo.Id);

            result.Result.Should().BeOfType<OkObjectResult>();

            var okResult = (OkObjectResult)result!.Result!;
            var todo = (Todo)okResult!.Value!;
            todo.Should().Be(Todo);
        }
    }
}
