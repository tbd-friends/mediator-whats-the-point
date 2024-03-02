using Data;
using Data.Models;
using Mediator;
using Microsoft.EntityFrameworkCore;

namespace WithMediator.Commands;

public class AddTodo
{
    public record Command(string Title, string Description) : ICommand;

    public class Handler(IDbContextFactory<TodoDbContext> factory) : ICommandHandler<Command>
    {
        public ValueTask<Unit> Handle(Command command, CancellationToken cancellationToken)
        {
            using (var context = factory.CreateDbContext())
            {
                context.Todos.Add(new Todo()
                {
                    Title = command.Title,
                    Description = command.Description,
                    Added = DateTime.UtcNow
                });

                context.SaveChanges();
            }

            return Unit.ValueTask;
        }
    }
}