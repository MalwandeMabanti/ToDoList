using ToDoList.Models;

namespace ToDoList.Interfaces
{
    public interface ITodoService : IGenericService<Todo>
    {
        public bool TodoItemExists(long id);
        //List<Todo> GetAllTodos();
        //Task<Todo> GetTodoByIdAsync(int id);

        //Task<Todo> AddTodoAsync(Todo todo);
        //Task<Todo> UpdateTodoAsync(Todo todo);
        //Task DeleteTodoAsync(int id);
    }
}
