using DLA.Entities;
using Microsoft.EntityFrameworkCore;

namespace DLA.Data;

public class ToDoContext : DbContext
{
    public ToDoContext(DbContextOptions<ToDoContext> options)
        : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TaskEntity>(task =>
        {
            task.ToTable("Tasks").HasKey(t => t.Id);
        });
    }

    public DbSet<TaskEntity> Tasks { get; set; }
}