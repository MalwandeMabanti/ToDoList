using ToDoList.Models;

namespace ToDoList.Interfaces
{
    public interface ITodoService 
    {
        List<Todo> GetAll();
        List<Todo> GetTodosByUserId(string userId);
        Todo GetTodoById(int id, string userId);
        void AddTodo(Todo todo, string userId);
        void UpdateTodo(Todo todo, string userId);
        void DeleteTodo(int id, string userId);


        public bool TodoItemExists(long id);
    }
}

/*
        List<T> GetAllTodos();

        T GetTodoById(int id);

        void AddTodo(T entity);

        void UpdateTodo(T entity);

        void DeleteTodo(int id);
 */