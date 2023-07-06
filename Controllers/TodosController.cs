//using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using ToDoList.Interfaces;
using ToDoList.Models;

namespace ToDoList.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TodosController : ControllerBase
    {
        private readonly ITodoService _todoService;
        //string useThis = "35d1b892-75e4-4b2f-895e-558a24f495df";

        public TodosController(ITodoService todoService) 
        {
            _todoService = todoService;
        }

        /// GET: api/todos
        [HttpGet]
        public ActionResult<IEnumerable<Todo>> GetTodos()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            //string userId = useThis;
            var todos = _todoService.GetTodosByUserId(userId);

            return Ok(todos);
        }


        // GET: api/todos/{id}
        [HttpGet("{id}")]
        public ActionResult<Todo> GetTodo(int id)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            //string userId = useThis;
            var todo = _todoService.GetTodoById(id, userId);

            if (todo == null)
            {
                return NotFound();
            }

            return Ok(todo);
        }

        [HttpPost]
        public ActionResult<Todo> PostTodo(Todo todo)
        {
            try
            {
                string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                Console.WriteLine($"User ID: {userId}");

                todo.UserId = userId;

                _todoService.AddTodo(todo, userId);

                return CreatedAtAction(nameof(GetTodo), new { id = todo.Id }, todo);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }
            return Ok();
            
        }

        // PUT: api/todos/{id}
        [HttpPut("{id}")]
        public IActionResult PutTodo(int id, Todo todo)
        {
            if (id != todo.Id)
            {
                return BadRequest();
            }

            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            //string userId = useThis;
            _todoService.UpdateTodo(todo, userId);
            
            return NoContent();
        }

        // DELETE: api/todos/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteTodo(int id)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            //string userId = useThis;
            _todoService.DeleteTodo(id, userId);

            return NoContent();
        }


    }
}
