using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Data.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddTodoDb(this IServiceCollection services)
    {
        services.AddPooledDbContextFactory<TodoDbContext>((provider, configure) =>
            configure.UseSqlite(provider.GetRequiredService<IConfiguration>().GetConnectionString("todo"))
                .LogTo(Console.WriteLine));

        return services;
    }
}