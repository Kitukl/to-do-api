using MediatR;

namespace BLL.Commands.Task.Delete;

public record DeleteTask(Guid id) : IRequest<Guid>;
