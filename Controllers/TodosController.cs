//using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Interfaces;
using ToDoList.Models;

namespace ToDoList.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodosController : ControllerBase
    {
        //private static List<Todo> _todos = new List<Todo>();
        private readonly ITodoService _todoService;

        public TodosController(ITodoService todoService) 
        {
            _todoService = todoService;
        }

        // GET: api/todos
        [HttpGet]
        public IEnumerable<Todo> GetTodos()
        {
            return _todoService.GetAllTodos();
        }


        // GET: api/todos/{id}
        [HttpGet("{id}")]
        public ActionResult<Todo> GetTodo(int id)
        {
            var todo = _todoService.GetTodoById(id);

            if (todo == null)
            {
                return NotFound();
            }
            return Ok(todo);
        }

        [HttpPost]
        public ActionResult<Todo> PostTodo(Todo todo)
        {
            _todoService.AddTodo(todo);

            return CreatedAtAction(nameof(GetTodo), new { id = todo.Id }, todo);
        }

        /// PUT: api/todos/{id}
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutTodo(int id, Todo todo) 
        //{
        //    if (id == todo.Id) 
        //    {
        //        return BadRequest();
        //    }

        //   await _todoService.AddTodoAsync(todo);

        //    return NoContent();

        //}

        //// DELETE: api/todos/{id}
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteTodo(int id) 
        //{

        //    await _todoService.DeleteTodoAsync(id);

        //    return NoContent();
        //}
    }
}
