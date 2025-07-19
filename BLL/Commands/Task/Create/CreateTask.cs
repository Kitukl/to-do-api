using MediatR;

namespace BLL.Commands.Task.Create;

public record CreateTask(Guid id, string title, string description, string status, DateTime date) : IRequest<string>;
