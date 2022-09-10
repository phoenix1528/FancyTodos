using Domain;
using Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Todos.Queries
{
    public class GetTodos
    {
        public class Query : IRequest<IEnumerable<Todo>> { }

        public class Handler : IRequestHandler<Query, IEnumerable<Todo>>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<IEnumerable<Todo>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _context.Todos.ToListAsync(cancellationToken).ConfigureAwait(false);
            }
        }
    }
}
