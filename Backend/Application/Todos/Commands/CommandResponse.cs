using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Todos.Commands
{
    public class CommandResponse
    {
        public IEnumerable<ValidationFailure> ValidationErrors { get; } = Enumerable.Empty<ValidationFailure>();
        public bool IsSuccessful => !ValidationErrors.Any();

        public CommandResponse() { }

        public CommandResponse(IEnumerable<ValidationFailure> validationErrors)
        {
            ValidationErrors = validationErrors;
        }


    }
}
