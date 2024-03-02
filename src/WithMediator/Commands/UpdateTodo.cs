using Data;
using Mediator;
using Microsoft.EntityFrameworkCore;

namespace WithMediator.Commands;

public class UpdateTodo
{
    public record Command(int Id, string Title, string Description) : ICommand<bool>;

    public class Handler(IDbContextFactory<TodoDbContext> factory) : ICommandHandler<Command, bool>
    {
        public async ValueTask<bool> Handle(Command command, CancellationToken cancellationToken)
        {
            await using var context = await factory.CreateDbContextAsync(cancellationToken);

            var todo = await context.Todos.FindAsync(command.Id, cancellationToken);

            if (todo is null)
            {
                return false;
            }

            todo.Title = command.Title;
            todo.Description = command.Description;

            await context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}