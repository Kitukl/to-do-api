using DLA.Repositories.Contracts;
using FluentValidation;
using MediatR;

namespace BLL.Commands.Task.Create;

public class CreateTaskHandler : IRequestHandler<CreateTask, string>
{
    private readonly ITaskRepository _repository;
    private readonly IValidator<CreateTask> _validator;

    public CreateTaskHandler(ITaskRepository repository, IValidator<CreateTask> validator)
    {
        _repository = repository;
        _validator = validator;
    }

    public async Task<string> Handle(CreateTask request, CancellationToken cancellationToken)
    {
        var validation = await _validator.ValidateAsync(request, cancellationToken);
        
        if (!validation.IsValid)
        {
            throw new Exception("Invalid date");
        }
        
        await _repository.Add(request.id, request.title, request.description, request.status, request.date);
        return request.title;
    }
}