using DLA.Entities;

namespace DLA.Repositories.Contracts;

public interface ITaskRepository
{
    public Task Add(Guid id, string title, string description, string status, DateTime date);
    public Task Change(Guid id, string title, string description, string status, DateTime date);
    public Task Delete(Guid id);
    public Task<List<TaskEntity>> GetAll();
}