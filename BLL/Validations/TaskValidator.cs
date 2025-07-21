using BLL.Commands.Task.Create;
using BLL.Commands.Task.Update;
using FluentValidation;

namespace BLL.Validations;

public class TaskValidatorBase<T> : AbstractValidator<T>
{
    private static readonly List<string> AllowedStatuses = new() { "Todo", "In Progress", "Done" };

    public TaskValidatorBase()
    {
        RuleFor(task => (string)task.GetType().GetProperty("title").GetValue(task))
            .NotEmpty().WithMessage("Title is required")
            .MinimumLength(5).WithMessage("Title must be at least 5 characters")
            .MaximumLength(50).WithMessage("Title must be at most 50 characters");

        RuleFor(task => (string)task.GetType().GetProperty("description").GetValue(task))
            .NotEmpty().WithMessage("Description is required")
            .MinimumLength(10).WithMessage("Description must be at least 10 characters")
            .MaximumLength(200).WithMessage("Description must be at most 200 characters");

        RuleFor(task => (string)task.GetType().GetProperty("status").GetValue(task))
            .Must(status => AllowedStatuses.Contains(status))
            .WithMessage($"Status must be one of: {string.Join(", ", AllowedStatuses)}");

        RuleFor(task => (DateTime)task.GetType().GetProperty("date").GetValue(task))
            .Must(date => date.Date >= DateTime.Today)
            .WithMessage("Date cannot be earlier than today");
    }
}

public class CreateTaskValidator : TaskValidatorBase<CreateTask> { }
public class UpdateTaskValidator : TaskValidatorBase<UpdateTask> { }
