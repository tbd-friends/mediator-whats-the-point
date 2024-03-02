using Data;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using NonMediator.Models;

namespace NonMediator.Services;

public class TodoService(IDbContextFactory<TodoDbContext> factory)
{
    public void AddTodo(TodoInputModel model)
    {
        using (var context = factory.CreateDbContext())
        {
            context.Todos.Add(
                new Todo { Title = model.Title, Description = model.Description, Added = DateTime.UtcNow });

            context.SaveChanges();
        }
    }
    
    public bool UpdateTodo(int id, TodoInputModel model)
    {
        using (var context = factory.CreateDbContext())
        {
            var todo = context.Todos.Find(id);

            if (todo is null)
            {
                return false;
            }

            todo.Title = model.Title;
            todo.Description = model.Description;
            todo.Updated = DateTime.UtcNow;

            context.SaveChanges();

            return true;
        }
    }
    
    public bool DeleteTodo(int id)
    {
        using (var context = factory.CreateDbContext())
        {
            var todo = context.Todos.Find(id);

            if (todo is null)
            {
                return false;
            }

            context.Todos.Remove(todo);
            context.SaveChanges();

            return true;
        }
    }
}