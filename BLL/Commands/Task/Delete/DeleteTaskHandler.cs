using DLA.Repositories.Contracts;
using MediatR;

namespace BLL.Commands.Task.Delete;

public class DeleteTaskHandler : IRequestHandler<DeleteTask, Guid>
{
    private readonly ITaskRepository _repository;

    public DeleteTaskHandler(ITaskRepository repository)
    {
        _repository = repository;
    }

    public async Task<Guid> Handle(DeleteTask request, CancellationToken cancellationToken)
    {
        await _repository.Delete(request.id);
        return request.id;
    }
}