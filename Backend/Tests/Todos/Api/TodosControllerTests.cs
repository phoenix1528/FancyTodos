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
using Shared.Dtos.Todos;

namespace Tests.Todos.Api
{
    public class TodosControllerTests : IClassFixture<TodosControllerTestsFixture>
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly TodosControllerTestsFixture _fixture;

        public TodosControllerTests(TodosControllerTestsFixture fixture)
        {

            _mediatorMock = new Mock<IMediator>();
            _fixture = fixture;
        }

        [Fact]
        [Trait("Category", "Unit")]
        public async Task GetTodos_WhenNoTodosExist_Returns200StatusCodeAndEmptyListS()
        {
            _mediatorMock.Setup(x => x.Send(It.IsAny<GetTodos.Query>(), default))
                .ReturnsAsync(Enumerable.Empty<Todo>());

            var controller = new TodosController(_mediatorMock.Object, _fixture.CommandResponseHandler);

            var result = await controller.GetTodosAsync();


            result.Result.Should().BeOfType<OkObjectResult>();

            var okResult = (OkObjectResult)result!.Result!;
            var todos = (IEnumerable<Todo>)okResult!.Value!;
            todos.Should().BeEmpty();
        }

        [Fact]
        [Trait("Category", "Unit")]
        public async Task GetTodos_WhenTodosExist_Returns200StatusCodeAndAllTodos()
        {
            _mediatorMock.Setup(x => x.Send(It.IsAny<GetTodos.Query>(), default))
                .ReturnsAsync(_fixture.Todos);

            var controller = new TodosController(_mediatorMock.Object, _fixture.CommandResponseHandler);

            var result = await controller.GetTodosAsync();

            result.Result.Should().BeOfType<OkObjectResult>();

            var okResult = (OkObjectResult)result!.Result!;
            var todos = (IEnumerable<Todo>)okResult!.Value!;
            todos.Should().HaveCount(_fixture.Todos.Count());
        }


        [Fact]
        [Trait("Category", "Unit")]
        public async Task GetTodo_WhenTodoWasNotFound_Returns404StatusCodeAndNull()
        {
            _mediatorMock.Setup(x => x.Send(It.IsAny<GetTodo.Query>(), default))
                .ReturnsAsync((Todo?)null);

            var controller = new TodosController(_mediatorMock.Object, _fixture.CommandResponseHandler);

            var result = await controller.GetTodoAsync(Guid.NewGuid());

            result.Result.Should().BeOfType<NotFoundObjectResult>();

            var okResult = (NotFoundObjectResult)result!.Result!;
            var todo = (Todo)okResult!.Value!;
            todo.Should().BeNull();
        }

        [Fact]
        [Trait("Category", "Unit")]
        public async Task GetTodo_WhenTodoExists_ReturnsTodoAnd200StatusCode()
        {
            _mediatorMock.Setup(x => x.Send(It.IsAny<GetTodo.Query>(), default))
                .ReturnsAsync(_fixture.Todo);

            var controller = new TodosController(_mediatorMock.Object, _fixture.CommandResponseHandler);

            var result = await controller.GetTodoAsync(_fixture.Todo.Id);

            result.Result.Should().BeOfType<OkObjectResult>();

            var okResult = (OkObjectResult)result!.Result!;
            var todo = (Todo)okResult!.Value!;
            todo.Should().Be(_fixture.Todo);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public async Task CreateTodo_WhenSuccessResponse_Returns201StatusCode()
        {
            _mediatorMock.Setup(x => x.Send(It.IsAny<CreateTodo.Command>(), default))
                .ReturnsAsync(_fixture.SuccessCommandResponse);

            var controller = new TodosController(_mediatorMock.Object, _fixture.CommandResponseHandler);

            var result = await controller.CreateTodoAsync(_fixture.CreateTodoDto);
            result.Should().BeOfType<StatusCodeResult>();

            var statusCodeResult = (StatusCodeResult)result;
            statusCodeResult.StatusCode.Should().Be(201);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public async Task CreateTodo_WhenFailureResponseWithValidationErrors_Returns400StatusCodeAndValidationErrors()
        {
            _mediatorMock.Setup(x => x.Send(It.IsAny<CreateTodo.Command>(), default))
                .ReturnsAsync(_fixture.FailureCommandResponseWithUnspecifiedValidationErrors);

            var controller = new TodosController(_mediatorMock.Object, _fixture.CommandResponseHandler);

            var result = await controller.CreateTodoAsync(_fixture.InvalidCreateTodoDto);

            result.Should().BeOfType<BadRequestObjectResult>();

            var badRequestObjectResult = (BadRequestObjectResult)result;
            var validationFailures = (IEnumerable<ValidationFailure>)badRequestObjectResult!.Value!;

            validationFailures.Should().BeEquivalentTo(_fixture.UnspecifiedValidationErrors);
            validationFailures.Should().HaveCount(_fixture.UnspecifiedValidationErrors.Count());
        }

        [Fact]
        [Trait("Category", "Unit")]
        public async Task EditTodo_WhenSuccessResponse_Returns204StatusCode()
        {
            _mediatorMock.Setup(x => x.Send(It.IsAny<EditTodo.Command>(), default))
                .ReturnsAsync(_fixture.SuccessCommandResponse);

            var controller = new TodosController(_mediatorMock.Object, _fixture.CommandResponseHandler);

            var result = await controller.EditTodoAsync(_fixture.EditTodoDto);
            result.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        [Trait("Category", "Unit")]
        public async Task EditTodo_WhenFailureResponseWhereItemNotExists_Returns404StatusCodeAndId()
        {
            _mediatorMock.Setup(x => x.Send(It.IsAny<EditTodo.Command>(), default))
                .ReturnsAsync(_fixture.FailureCommandResponseWhereItemNotExists);

            var controller = new TodosController(_mediatorMock.Object, _fixture.CommandResponseHandler);

            var result = await controller.EditTodoAsync(_fixture.EditTodoDto);

            result.Should().BeOfType<NotFoundObjectResult>();

            var notFoundObjectResult = (NotFoundObjectResult)result;

            notFoundObjectResult.Value.Should().Be(_fixture.InvalidEditTodoDto.Id);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public async Task EditTodo_WhenFailureResponseWithValidationErrors_Returns404StatusCodeAndId()
        {
            _mediatorMock.Setup(x => x.Send(It.IsAny<EditTodo.Command>(), default))
                .ReturnsAsync(_fixture.FailureCommandResponseWithUnspecifiedValidationErrors);

            var controller = new TodosController(_mediatorMock.Object, _fixture.CommandResponseHandler);

            var result = await controller.EditTodoAsync(_fixture.EditTodoDto);

            result.Should().BeOfType<BadRequestObjectResult>();

            var badRequestObjectResult = (BadRequestObjectResult)result;
            var validationFailures = (IEnumerable<ValidationFailure>)badRequestObjectResult!.Value!;

            validationFailures.Should().BeEquivalentTo(_fixture.UnspecifiedValidationErrors);
            validationFailures.Should().HaveCount(_fixture.UnspecifiedValidationErrors.Count());
        }

        [Trait("Category", "Unit")]
        public async Task DeleteTodo_WhenSuccessResponse_Returns204StatusCode()
        {
            _mediatorMock.Setup(x => x.Send(It.IsAny<DeleteTodo.Command>(), default))
                .ReturnsAsync(_fixture.SuccessCommandResponse);

            var controller = new TodosController(_mediatorMock.Object, _fixture.CommandResponseHandler);

            var result = await controller.EditTodoAsync(_fixture.EditTodoDto);
            result.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        [Trait("Category", "Unit")]
        public async Task DeleteTodo_WhenFailureResponseWhereItemNotExists_Returns404StatusCodeAndId()
        {
            _mediatorMock.Setup(x => x.Send(It.IsAny<DeleteTodo.Command>(), default))
                .ReturnsAsync(_fixture.FailureCommandResponseWhereItemNotExists);

            var controller = new TodosController(_mediatorMock.Object, _fixture.CommandResponseHandler);

            var result = await controller.DeleteTodoAsync(_fixture.Todo.Id);

            result.Should().BeOfType<NotFoundObjectResult>();

            var notFoundObjectResult = (NotFoundObjectResult)result;

            notFoundObjectResult.Value.Should().Be(_fixture.Todo.Id);
        }
    }
}
