using Microsoft.EntityFrameworkCore;
using ToDoList.Data;
using ToDoList.Interfaces;
using ToDoList.Models;
using static ToDoList.Services.TodoService;

namespace ToDoList.Services
{
    public class TodoService : GenericService<Todo>, ITodoService
    {
        public TodoService(ApplicationDbContext context)
            : base(context)
        {
            
        }

        //public async Task<Todo> GetTodoByIdAsync(int id)
        //{
        //    return await _context.Todo.FindAsync(id);
        //}

        //public async Task<Todo> AddTodoAsync(Todo todo)
        //{
        //    var result = await _context.Todo.AddAsync(todo);
        //    await _context.SaveChangesAsync();
        //    return result.Entity;
        //}

        //public async Task<Todo> UpdateTodoAsync(Todo todo)
        //{
        //    _context.Entry(todo).State = EntityState.Modified;
        //    await _context.SaveChangesAsync();
        //    return todo;
        //}

        //public async Task DeleteTodoAsync(int id)
        //{
        //    var todo = await _context.Todo.FindAsync(id); 
        //    if(todo != null) 
        //    {
        //        _context.Todo.Remove(todo);
        //        await _context.SaveChangesAsync();
        //    }
        //}

    }
}
