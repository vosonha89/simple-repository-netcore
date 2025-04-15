using DevNetCore.SimpleRepositoryExample.Entity;
using Microsoft.EntityFrameworkCore;

namespace DevNetCore.SimpleRepositoryExample;

public class SimpleRepositoryExampleContext(DbContextOptions<SimpleRepositoryExampleContext> options)
    : DbContext(options)
{
    public DbSet<ToDo> ToDos { get; set; }
}