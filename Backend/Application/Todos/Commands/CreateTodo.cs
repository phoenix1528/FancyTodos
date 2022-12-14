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
        public class Command : IRequest<ICreateCommandResponse>
        {
            public Command(CreateTodoDto createTodoDto)
            {
                CreateTodoDto = createTodoDto;
            }

            public CreateTodoDto CreateTodoDto { get; private set; }
        }

        public class Handler : IRequestHandler<Command, ICreateCommandResponse>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<ICreateCommandResponse> Handle(Command request, CancellationToken cancellationToken)
            {
                try
                {
                    var todo = Todo.Create(_mapper.Map<Todo>(request.CreateTodoDto));

                    _context.Todos.Add(todo);
                    await _context.SaveChangesAsync().ConfigureAwait(false);

                    return new SuccessCommandResponse(todo.Id);
                }
                catch (ValidationException ex)
                {
                    return new FailureCommandResponse(ex.Errors);
                }
            }
        }
    }
}
