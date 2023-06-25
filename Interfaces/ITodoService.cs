using ToDoList.Models;

namespace ToDoList.Interfaces
{
    public interface ITodoService : IGenericService<Todo>
    {
        public bool TodoItemExists(long id);
    }
}
