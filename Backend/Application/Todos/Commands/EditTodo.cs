using AutoMapper;
using Domain;
using FluentValidation;
using Infrastructure;
using MediatR;
using Shared.Dtos.Todos;

namespace Application.Todos.Commands
{
    public class EditTodo
    {
        public class Command : IRequest<ICommandResponse>
        {
            public EditTodoDto EditTodoDto { get; private set; }

            public Command(EditTodoDto editTodoDto)
            {
                EditTodoDto = editTodoDto;
            }
        }

        public class Handler : IRequestHandler<Command, ICommandResponse>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<ICommandResponse> Handle(Command request, CancellationToken cancellationToken)
            {
                var todo = await _context.Todos.FindAsync(request.EditTodoDto.Id).ConfigureAwait(false);

                if (todo == null)
                {
                    return new FailureCommandResponse(false);
                }

                try
                {
                    todo.Update(_mapper.Map<Todo>(request.EditTodoDto));

                    await _context.SaveChangesAsync().ConfigureAwait(false);
                }
                catch (ValidationException ex)
                {
                    return new FailureCommandResponse(ex.Errors);
                }

                return new SuccessCommandResponse();
            }
        }
    }
}
