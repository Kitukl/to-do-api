using BLL.Validations;
using DLA.Repositories.Contracts;
using FluentValidation;
using MediatR;

namespace BLL.Commands.Task.Update;

public class UpdateTaskHandler : IRequestHandler<UpdateTask, string>
{
    private readonly ITaskRepository _repository;
    private readonly IValidator<UpdateTask> _validator;

    public UpdateTaskHandler(ITaskRepository repository, IValidator<UpdateTask> validator)
    {
        _repository = repository;
        _validator = validator;
    }

    public async Task<string> Handle(UpdateTask request, CancellationToken cancellationToken)
    {
        var validation = await _validator.ValidateAsync(request, cancellationToken);

        if (!validation.IsValid)
        {
            throw new ValidationException(validation.Errors);
        }
        
        await _repository.Change(request.id, request.title, request.description, request.status, request.date);
        return request.title;
    }
}