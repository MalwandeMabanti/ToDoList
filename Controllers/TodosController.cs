//using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using ToDoList.Interfaces;
using ToDoList.Models;
using ToDoList.Services;

namespace ToDoList.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class TodosController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ITodoService _todoService;
        private readonly IAzureBlobService _azureBlobService;
        //string useThis = "35d1b892-75e4-4b2f-895e-558a24f495df";

        public TodosController(IConfiguration configuration, ITodoService todoService, IAzureBlobService azureBlobService) 
        {
            _configuration = configuration;
            _todoService = todoService;
            _azureBlobService = azureBlobService;
        }

        [HttpGet("all")]
        public ActionResult<IEnumerable<Todo>> GetAll() 
        {
            var allTodos = _todoService.GetAll();

            return Ok(allTodos);
        }



        // GET: api/todos
        [Authorize]
        [HttpGet]
        public ActionResult<IEnumerable<Todo>> GetTodos()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            //string userId = useThis;
            var todos = _todoService.GetTodosByUserId(userId);

            return Ok(todos);
        }


        // GET: api/todos/{id}
        [Authorize]
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

        [Authorize]
        [HttpPost, Consumes("multipart/form-data")]
        public async Task<ActionResult<Todo>> PostTodo([FromForm]TodoViewModel todoViewModel)
        {
            if (todoViewModel == null)
            {
                return BadRequest("Invalid request");
            }

            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string imageUrl = null;

            // Retrieve the container name from your configuration
            string containerName = _configuration.GetValue<string>("Azure:BlobStorage:ContainerName");

            if (todoViewModel.Image != null)
            {
                Console.WriteLine(Guid.NewGuid().ToString() + " Guid.NewGuid().ToString()");
                Console.WriteLine(Path.GetExtension(todoViewModel.Image.FileName) + " Path.GetExtension(todoViewModel.Image.FileName)");
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(todoViewModel.Image.FileName);

                // Pass the container name to the UploadFileAsync method
                imageUrl = await _azureBlobService.UploadFileAsync(containerName, todoViewModel.Image, fileName);
            }

            // After that, you can create the Todo object and populate the fields with the ViewModel data
            Todo todo = new Todo
            {
                Title = todoViewModel.Title,
                Description = todoViewModel.Description,
                UserId = userId,
                ImageUrl = imageUrl
            };

            _todoService.AddTodo(todo, userId);

            return CreatedAtAction(nameof(GetTodo), new { id = todo.Id }, todo);
        }

        // PUT: api/todos/{id}
        [Authorize]
        [HttpPut("{id}"), Consumes("multipart/form-data")]
        public async Task<IActionResult> PutTodo(int id, [FromForm]TodoViewModel todoViewModel)
        {
            if (id != todoViewModel.Id)
            {
                return BadRequest();
            }

            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);


            string imageUrl = null;

            // Retrieve the container name from your configuration
            string containerName = _configuration.GetValue<string>("Azure:BlobStorage:ContainerName");

            if (todoViewModel.Image != null)
            {
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(todoViewModel.Image.FileName);

                // Pass the container name to the UploadFileAsync method
                imageUrl = await _azureBlobService.UploadFileAsync(containerName, todoViewModel.Image, fileName);
            }

            // After that, you can create the Todo object and populate the fields with the ViewModel data
            Todo todo = new Todo
            {
                Id = todoViewModel.Id,
                Title = todoViewModel.Title,
                Description = todoViewModel.Description,
                UserId = userId,
                ImageUrl = imageUrl
            };


            _todoService.UpdateTodo(todo, userId);
           
            
            return NoContent();
        }

        // DELETE: api/todos/{id}
        [Authorize]
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
