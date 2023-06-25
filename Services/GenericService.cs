using Microsoft.EntityFrameworkCore;
using ToDoList.Interfaces;
using ToDoList.Models;

namespace ToDoList.Services
{
    public class GenericService<T> : IGenericService<T>
        where T : class
    {
        private readonly DbContext context;

        public GenericService(DbContext context)
        {
            this.context = context;
        }

        public List<T> GetAllTodos()
        {
            return this.context.Set<T>().ToList();
        }

        public T GetTodoById(int id)
        {
            return this.context.Set<T>().Find(id);
        }

        public void AddTodo(T entity)
        {
            this.context.Set<T>().Add(entity);
            this.context.SaveChanges();
        }

        public void UpdateTodo(T entity)
        {
            this.context.Set<T>().Attach(entity);
            this.context.Entry(entity).State = EntityState.Modified;
            this.context.SaveChanges();
        }

        public void DeleteTodo(int id)
        {
            T? entity = this.context.Set<T>().Find(id);
            this.context.Set<T>().Remove(entity!);
            this.context.SaveChanges();
        }


    }

}
