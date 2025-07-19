using MediatR;

namespace BLL.Commands.Task.Update;

public record UpdateTask(Guid id, string title, string description, string status, DateTime date) : IRequest<string>;