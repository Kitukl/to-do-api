using DLA.Repositories.Contracts;
using MediatR;

namespace BLL.Commands.Task.Create;

public class CreateTaskHandler : IRequestHandler<CreateTask, string>
{
    private readonly ITaskRepository _repository;

    public CreateTaskHandler(ITaskRepository repository)
    {
        _repository = repository;
    }

    public async Task<string> Handle(CreateTask request, CancellationToken cancellationToken)
    {
        await _repository.Add(request.id, request.title, request.description, request.status, request.date);
        return request.title;
    }
}