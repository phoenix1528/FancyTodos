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
        public class Command : IRequest<ICommandResponse>
        {
            public Guid Id { get; private set; }

            public Command(Guid id)
            {
                Id = id;
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
                var todo = await _context.Todos.FindAsync(request.Id).ConfigureAwait(false);

                if (todo == null)
                {
                    return new FailureCommandResponse(false);
                }

                _context.Remove(todo);
                await _context.SaveChangesAsync().ConfigureAwait(false);

                return new SuccessCommandResponse();
            }
        }
    }
}
