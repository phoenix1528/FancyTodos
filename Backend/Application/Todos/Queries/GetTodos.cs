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
        public class Query : IRequest<List<Todo>> { }

        public class Handler : IRequestHandler<Query, List<Todo>>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<List<Todo>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _context.Todos.ToListAsync(cancellationToken).ConfigureAwait(false);
            }
        }
    }
}
