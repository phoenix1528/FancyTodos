using AutoMapper;
using Domain;
using Infrastructure;
using MediatR;
using Shared.Dtos.Todos;

namespace Application.Todos.Commands
{
    public class EditTodo
    {
        public class Command : IRequest
        {
            public EditTodoDto EditTodoDto { get; private set; }

            public Command(EditTodoDto editTodoDto)
            {
                EditTodoDto = editTodoDto;
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
                var todo = await _context.Todos.FindAsync(request.EditTodoDto.Id);

                _mapper.Map(request.EditTodoDto, todo);

                await _context.SaveChangesAsync();

                return Unit.Value;
            }
        }
    }
}
