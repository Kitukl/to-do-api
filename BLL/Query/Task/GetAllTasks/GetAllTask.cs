using BLL.DTOs;
using MediatR;

namespace BLL.Query.Task.GetAllTasks;

public record GetAllTask : IRequest<List<TaskDTOs>>;