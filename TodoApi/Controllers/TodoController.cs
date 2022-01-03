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
        // GET: api/<TodoController>
        [Route("GetAll")]
        [HttpGet]
        public async Task<List<Todo>> Get()
        {
            using var context = new TodoDbContext();
            return await context.Todos.ToListAsync();
        }

        // GET api/<TodoController>/5
        [Route("GetTodo/{id}")]
        [HttpGet]
        public Todo Get(int id)
        {
            using var context = new TodoDbContext();
            return context.Todos.Where(x => x.SerialNo == id).FirstOrDefault();
        }

        // POST api/<TodoController>
        [HttpPost]
        public void Post([FromBody] Todo value)
        {
            using var context = new TodoDbContext();
            context.Add(value);

            context.SaveChanges();
        }

        [Route("UpdateToDo")]
        [HttpPost()]
        public void Update([FromBody] Todo value)
        {
            using var context = new TodoDbContext();
            var toDo= context.Todos.Where(x => x.SerialNo == value.SerialNo).FirstOrDefault();
            if (toDo != null)
            {
                toDo.Title = value.Title;
                toDo.Description = value.Description;   
                toDo.IsCompleted = value.IsCompleted;   

                context.SaveChanges();
            }
        }

        // DELETE api/<TodoController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
