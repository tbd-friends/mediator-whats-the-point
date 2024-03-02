using Data;
using Mediator;
using Microsoft.EntityFrameworkCore;

namespace WithMediator.Commands;

public class DeleteTodo
{
    public record Command(int Id) : ICommand<bool>;

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

            context.Todos.Remove(todo);

            await context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}