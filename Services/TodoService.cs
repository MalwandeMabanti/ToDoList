using Microsoft.EntityFrameworkCore;
using ToDoList.Data;
using ToDoList.Interfaces;
using ToDoList.Models;
using static ToDoList.Services.TodoService;

namespace ToDoList.Services
{
    public class TodoService : GenericService<Todo>, ITodoService
    {
        private readonly ApplicationDbContext _context;

        public TodoService(ApplicationDbContext context)
            : base(context)
        {
            _context = context;
        }

        public bool TodoItemExists(long id)
        {
            return this._context.Todo.Any(x => x.Id == id);
        }
    }
}
