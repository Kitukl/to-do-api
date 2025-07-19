using DLA.Repositories.Contracts;
using MediatR;

namespace BLL.Commands.Task.Update;

public class UpdateTaskHandler : IRequestHandler<UpdateTask, string>
{
    private readonly ITaskRepository _repository;

    public UpdateTaskHandler(ITaskRepository repository)
    {
        _repository = repository;
    }

    public async Task<string> Handle(UpdateTask request, CancellationToken cancellationToken)
    {
        await _repository.Change(request.id, request.title, request.description, request.status, request.date);
        return request.title;
    }
}