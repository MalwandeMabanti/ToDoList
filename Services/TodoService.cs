using Microsoft.EntityFrameworkCore;
using ToDoList.Data;
using ToDoList.Interfaces;
using ToDoList.Models;
using static ToDoList.Services.TodoService;

namespace ToDoList.Services
{
    public class TodoService : ITodoService
    {
        private readonly ApplicationDbContext context;

        public TodoService(ApplicationDbContext context)
            
        {
            this.context = context;
        }

        public List<Todo> GetAll()
        {
            return this.context.Set<Todo>().ToList();
        }


        public List<Todo> GetTodosByUserId(string userId)
        {
            return this.context.Set<Todo>().Where(todo => todo.UserId == userId).ToList();
        }

        public Todo GetTodoById(int id, string userId)
        {
            return this.context.Set<Todo>().SingleOrDefault(todo => todo.Id == id && todo.UserId == userId);
        }

        public void AddTodo(Todo todo, string userId)
        {
            todo.UserId = userId;
            this.context.Set<Todo>().Add(todo);
            this.context.SaveChanges();
        }

        public void UpdateTodo(Todo todo, string userId)
        {
            var existingTodo = this.context.Set<Todo>().SingleOrDefault(t => t.Id == todo.Id && t.UserId == userId);
            if (existingTodo != null)
            {
                this.context.Entry(existingTodo).CurrentValues.SetValues(todo);
                this.context.SaveChanges();
            }
        }

        public void DeleteTodo(int id, string userId)
        {
            var todo = this.context.Set<Todo>().SingleOrDefault(t => t.Id == id && t.UserId == userId);
            if (todo != null)
            {
                this.context.Set<Todo>().Remove(todo);
                this.context.SaveChanges();
            }
        }




        public bool TodoItemExists(long id)
        {
            return this.context.Todo.Any(x => x.Id == id);
        }
    }
}
