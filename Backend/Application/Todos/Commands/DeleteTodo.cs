using AutoMapper;
using Infrastructure;
using MediatR;
using Shared.Dtos.Todos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Todos.Commands
{
    public class DeleteTodo
    {
        public class Command : IRequest
        {
            public Guid Id { get; private set; }

            public Command(Guid id)
            {
                Id = id;
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
                var todo = await _context.Todos.FindAsync(request.Id);

                if(todo != null)
                {
                    _context.Remove(todo);
                    await _context.SaveChangesAsync();
                }

                return Unit.Value;
            }
        }
    }
}
