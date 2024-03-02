using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Data;

public class TodoDbContext(DbContextOptions<TodoDbContext> options) : DbContext(options)
{
    public DbSet<Todo> Todos => Set<Todo>();
}