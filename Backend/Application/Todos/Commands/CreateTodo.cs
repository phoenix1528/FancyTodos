using AutoMapper;
using Domain;
using Infrastructure;
using MediatR;
using Shared.Dtos.Todos;

namespace Application.Todos.Commands
{
    public class CreateTodo
    {
        public class Command : IRequest
        {
            public CreateTodoDto CreateTodoDto { get; private set; }

            public Command(CreateTodoDto createTodoDto) 
            {
                CreateTodoDto = createTodoDto;
            }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var todo = _mapper.Map<Todo>(request.CreateTodoDto);

                _context.Todos.Add(todo);
                await _context.SaveChangesAsync();

                return Unit.Value;
            }
        }
    }
}
