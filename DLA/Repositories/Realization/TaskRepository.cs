using DLA.Data;
using DLA.Entities;
using DLA.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace DLA.Repositories.Realization;

public class TaskRepository : ITaskRepository
{
    private readonly ToDoContext _context;
    
    public TaskRepository(ToDoContext context)
    {
        _context = context;
    }
    
    public async Task Create(Guid id, string title, string description, string status, DateTime date)
    {
        var task = new TaskEntity()
        {
            Id = id,
            Title = title,
            Description = description,
            Status = status,
            Date = date
        };
        await _context.Tasks.AddAsync(task);
        await _context.SaveChangesAsync();
    }

    public async Task Change(Guid id, string title, string description, string status, DateTime date)
    {
        await _context.Tasks
            .Where(task => task.Id == id)
            .ExecuteUpdateAsync(s => s.SetProperty(
                    task => task.Title, title                
                )
                .SetProperty(task => task.Description, description)
                .SetProperty(task => task.Status, status)
                .SetProperty(task => task.Date, date));
        await _context.SaveChangesAsync();
    }

    public async Task Delete(Guid id)
    {
        await _context.Tasks
            .Where(task => task.Id == id)
            .ExecuteDeleteAsync();
        await _context.SaveChangesAsync();
    }

    public async Task<List<TaskEntity>> GetAll()
    {
        return await _context.Tasks.ToListAsync();
    }
}