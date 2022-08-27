using Domain;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class TodosController : BaseApiController
    {
        private readonly DataContext _context;

        public TodosController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Todo>>> GetTodos()
        {
            return Ok(await _context.Todos.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Todo>> GetTodo(Guid id)
        {
            var todo = await _context.Todos.FindAsync(id);

            return StatusCode(todo != null ? 200 : 404, todo);
        }
    }
}
