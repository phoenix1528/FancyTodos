using AutoMapper;
using Domain;
using FluentValidation;
using Infrastructure;
using MediatR;
using Shared.Dtos.Todos;

namespace Application.Todos.Commands
{
    public class CreateTodo
    {
        public class Command : IRequest<CommandResponse>
        {
            public CreateTodoDto CreateTodoDto { get; private set; }

            public Command(CreateTodoDto createTodoDto)
            {
                CreateTodoDto = createTodoDto;
            }
        }

        public class Handler : IRequestHandler<Command, CommandResponse>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<CommandResponse> Handle(Command request, CancellationToken cancellationToken)
            {
                try
                {
                    var todo = Todo.Create(_mapper.Map<Todo>(request.CreateTodoDto));

                    _context.Todos.Add(todo);
                    await _context.SaveChangesAsync().ConfigureAwait(false);
                }
                catch (ValidationException ex)
                {
                    return new CommandResponse(ex.Errors);
                }

                return new CommandResponse();
            }
        }
    }
}
