using API.ResponseHandlers;
using Application.Todos.Commands;
using Domain;
using FluentValidation.Results;
using Shared.Dtos.Todos;

namespace Tests.Todos.Api
{
    public class ApiTestsFixture : IDisposable
    {
        public readonly ICommandResponseHandler CommandResponseHandler;
        public readonly IEnumerable<Todo> Todos;
        public readonly Todo Todo;
        public readonly FailureCommandResponse FailureCommandResponseWithUnspecifiedValidationErrors;
        public readonly SuccessCommandResponse SuccessCommandResponse;
        public readonly SuccessCommandResponse SuccessCommandResponseWithId;
        public readonly Guid TodoId;
        public readonly CreateTodoDto CreateTodoDto;
        public readonly CreateTodoDto InvalidCreateTodoDto;
        public readonly IEnumerable<ValidationFailure> UnspecifiedValidationErrors;
        public readonly EditTodoDto EditTodoDto;
        public readonly EditTodoDto InvalidEditTodoDto;
        public readonly FailureCommandResponse FailureCommandResponseWhereItemNotExists;

        public ApiTestsFixture()
        {
            CommandResponseHandler = new CommandResponseHandler();
            Todos = TodosDataHelper.GenerateTodos();
            Todo = TodosDataHelper.GenerateSingleTodo();
            FailureCommandResponseWithUnspecifiedValidationErrors = TodosDataHelper.GenerateFailureCommandResponseWithUnspecifiedValidationErrors();
            SuccessCommandResponse = TodosDataHelper.GenerateSuccessCommandResponse();
            SuccessCommandResponseWithId = TodosDataHelper.GenerateSuccessCommandResponseWithId();
            TodoId = TodosDataHelper.GUID;
            CreateTodoDto = TodosDataHelper.GenerateCreateTodoDto();
            InvalidCreateTodoDto = TodosDataHelper.GenerateInvalidCreateTodoDto();
            UnspecifiedValidationErrors = TodosDataHelper.GenerateUnspecifiedValidationErrors();
            EditTodoDto = TodosDataHelper.GenerateEditTodoDto();
            InvalidEditTodoDto = TodosDataHelper.GenerateInvalidEditTodoDto();
            FailureCommandResponseWhereItemNotExists = TodosDataHelper.GenerateFailureCommandResponseWhereItemNotExists();
        }

        public void Dispose()
        {
        }
    }
}
