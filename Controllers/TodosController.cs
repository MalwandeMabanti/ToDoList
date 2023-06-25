//using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        // PUT: api/todos/{id}
        [HttpPut("{id}")]
        public IActionResult PutTodo(int id, Todo todo) 
        {
            //todo.Id = _todoService.GetTodoById(id);

            if (id != todo.Id) 
            {
                return BadRequest();
            }

            //try
            //{
            //    _todoService.AddTodo(todo);
            //}
            //catch (DbUpdateConcurrencyException) 
            //{
                
            //}

           _todoService.UpdateTodo(todo);

            return NoContent();

        }

        // DELETE: api/todos/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteTodo(int id)
        {
            var model = new Todo 
            { 
                Id = id
            };

            _todoService.DeleteTodo(model.Id);

            return NoContent();
        }
    }
}
