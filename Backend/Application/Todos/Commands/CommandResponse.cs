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
        public bool ItemExists { get;  }

        public bool Success => !ValidationErrors.Any() && ItemExists;
    }

    public class SuccessCommandResponse : ICommandResponse
    {
        public IEnumerable<ValidationFailure> ValidationErrors { get; private set; } = Enumerable.Empty<ValidationFailure>();
        public bool ItemExists { get; private set; } = true;
    }

    public class FailureCommandResponse : ICommandResponse
    {
        public IEnumerable<ValidationFailure> ValidationErrors { get; private set; } = Enumerable.Empty<ValidationFailure>();
        public bool ItemExists { get; private set; } = true;

        public FailureCommandResponse(bool itemExists)
        {
            ItemExists = itemExists;
        }

        public FailureCommandResponse(IEnumerable<ValidationFailure> validationErrors)
        {
            ValidationErrors = validationErrors;
        }
    }
}
