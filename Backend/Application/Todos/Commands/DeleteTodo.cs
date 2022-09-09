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
        public class Command : IRequest<CommandResponse>
        {
            public Guid Id { get; private set; }

            public Command(Guid id)
            {
                Id = id;
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
                var todo = await _context.Todos.FindAsync(request.Id).ConfigureAwait(false);

                if (todo == null)
                {
                    return new CommandResponse(false);
                }

                _context.Remove(todo);
                await _context.SaveChangesAsync().ConfigureAwait(false);

                return new CommandResponse(false);
            }
        }
    }
}
