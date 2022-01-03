using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApi.Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TodoApi.Controllers
{
    [Route("api")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly TodoDbContext _context;
        public TodoController(TodoDbContext context)
        {
            _context = context;
        }
        // GET: api/<TodoController>
        [Route("GetAll")]
        [HttpGet]
        public async Task<List<Todo>> Get()
        {
            return await _context.Todos.ToListAsync();
        }

        // GET api/<TodoController>/5
        [Route("GetTodo/{id}")]
        [HttpGet]
        public async Task<ActionResult<Todo>> Get(int id)
        {
            var todo = await _context.Todos.Where(x => x.SerialNo == id).FirstOrDefaultAsync();

            if (todo == null)
            {
                return NotFound();
            }

            return todo;
        }

        // POST api/<TodoController>
        [Route("CreateTodo")]
        [HttpPost]
        public async Task Create([FromBody] Todo value)
        {
            var serialNo = _context.Todos.Max(x => x.SerialNo);
            value.SerialNo = serialNo +1;
            value.CreatedDate = DateTime.Now;
            _context.Todos.Add(value);
            await _context.SaveChangesAsync();
        }

        [Route("UpdateTodo")]
        [HttpPost]
        public async Task Update([FromBody] Todo value)
        {
            var toDo= _context.Todos.Where(x => x.SerialNo == value.SerialNo).FirstOrDefault();
            if (toDo != null)
            {
                toDo.Title = value.Title;
                toDo.Description = value.Description;   
                toDo.IsCompleted = value.IsCompleted;   

                await _context.SaveChangesAsync();
            }
        }

        // DELETE api/<TodoController>/5
        [Route("DeleteToDo/{id}")]
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var toDo = _context.Todos.Where(x => x.SerialNo == id).FirstOrDefault();
            if (toDo == null)
            {
                return NotFound();
            }

            _context.Todos.Remove(toDo);

            var listToDo = _context.Todos.Where(x => x.SerialNo > id);
            await listToDo.ForEachAsync(x => --x.SerialNo);

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
