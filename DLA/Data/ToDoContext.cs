using DLA.Entities;
using Microsoft.EntityFrameworkCore;

namespace DLA.Data;

public class ToDoContext(DbContextOptions<ToDoContext> options) : DbContext(options)
{
    public DbSet<TaskEntity> Tasks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<TaskEntity>(entity =>
        {
            entity.HasKey(task => task.Id);
            entity.Property(task => task.Id).ValueGeneratedOnAdd();
            entity.Property(task => task.Title).IsRequired();
            entity.Property(task => task.Description).IsRequired();
            entity.Property(task => task.Status).IsRequired();
            entity.Property(task => task.Date).IsRequired();
        });
    }
}