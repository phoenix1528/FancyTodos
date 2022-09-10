using Domain;
using Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Todos.Queries
{
    public class GetTodo
    {
        public class Query : IRequest<Todo?> 
        {
            public Query(
                Guid id)
            {
                Id = id;
            }

            public Guid Id { get; private set; }
        }

        public class Handler : IRequestHandler<Query, Todo?>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<Todo?> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _context.Todos.FindAsync(request.Id).ConfigureAwait(false);
            }
        }
    }
}
