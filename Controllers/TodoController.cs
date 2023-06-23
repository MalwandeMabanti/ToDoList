//using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Models;

namespace ToDoList.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private static List<Todo> _todos = new List<Todo>();

        // GET: api/todos
        [HttpGet]
        public ActionResult<IEnumerable<Todo>> Get()
        {
            return _todos;
        }

        // GET: api/todos/{id}
        [HttpGet("{id}")]
        public ActionResult<Todo> Get(int id) 
        {
            var todo = _todos.FirstOrDefault(t => t.Id == id);
            if (todo == null) 
            {
                return NotFound();
            }
            return todo;
        }

        [HttpPost]
        public ActionResult<Todo> Post(Todo todo) 
        {
            _todos.Add(todo);
            return CreatedAtAction(nameof(Get), new {id =  todo.Id}, todo);
        }

        // PUT: api/todos/{id}
        [HttpPut("{id}")]
        public IActionResult Put(int id, Todo todo) 
        {
            if (id == todo.Id) 
            {
                return BadRequest();
            }

            var existingTodo = _todos.FirstOrDefault(t => t.Id == id);
            if (existingTodo != null) 
            {
                return NotFound();
            }

            existingTodo.Title = todo.Title;
            existingTodo.Description = todo.Description;
            existingTodo.Completed = todo.Completed;

            return NoContent();

        }

        // DELETE: api/todos/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id) 
        {
            var todo = _todos.FirstOrDefault(t =>t.Id == id);
            if (todo == null) 
            { 
                return NotFound();
            }

            _todos.Remove(todo);

            return NoContent();
        }
    }
}
