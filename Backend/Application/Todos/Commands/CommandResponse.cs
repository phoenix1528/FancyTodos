using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Todos.Commands
{
    public interface ICommandResponse
    {
        public IEnumerable<ValidationFailure> ValidationErrors { get; }
        public bool ItemExists { get; }
        public Guid ItemId { get; }

        public bool ValidationSuccess => !ValidationErrors.Any();
    }
    public interface ICreateCommandResponse : ICommandResponse { }

    public interface IEditCommandResponse : ICommandResponse { }

    public interface IDeleteCommandResponse : ICommandResponse { }

    public class SuccessCommandResponse : ICreateCommandResponse, IEditCommandResponse, IDeleteCommandResponse
    {
        public SuccessCommandResponse() { }

        public SuccessCommandResponse(Guid id)
        {
            ItemId = id;
        }

        public IEnumerable<ValidationFailure> ValidationErrors { get; private set; } = Enumerable.Empty<ValidationFailure>();
        public bool ItemExists { get; private set; } = true;
        public Guid ItemId { get; }
    }

    public class FailureCommandResponse : ICreateCommandResponse, IEditCommandResponse, IDeleteCommandResponse
    {
        public FailureCommandResponse(bool itemExists, Guid itemId)
        {
            ItemExists = itemExists;
            ItemId = itemId;
        }

        public FailureCommandResponse(IEnumerable<ValidationFailure> validationErrors)
        {
            ValidationErrors = validationErrors;
        }

        public IEnumerable<ValidationFailure> ValidationErrors { get; private set; } = Enumerable.Empty<ValidationFailure>();
        public bool ItemExists { get; private set; } = true;
        public Guid ItemId { get; }
    }
}
