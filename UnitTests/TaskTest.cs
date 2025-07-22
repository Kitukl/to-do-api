using BLL.Commands.Task.Create;
using BLL.Commands.Task.Delete;
using BLL.Commands.Task.Update;
using BLL.Validations;
using DLA.Repositories.Contracts;
using Moq;

public class TaskTests
{
    [Fact]
    public async Task InvalidCreate()
    {
        var mockRepo = new Mock<ITaskRepository>();
        var validator = new CreateTaskValidator();

        var request = new CreateTask(Guid.NewGuid(), "", "fd", "Donr", DateTime.Today);

        var handler = new CreateTaskHandler(mockRepo.Object, validator);
        
        await Assert.ThrowsAsync<FluentValidation.ValidationException>(async () =>
            await handler.Handle(request, CancellationToken.None)
        );

        mockRepo.Verify(r => r.Create(
            It.IsAny<Guid>(),
            It.IsAny<string>(),
            It.IsAny<string>(),
            It.IsAny<string>(),
            It.IsAny<DateTime>()
        ), Times.Never);
    }
    
    [Fact]
    public async Task ValidCreate()
    {
        var mockRepo = new Mock<ITaskRepository>();
        var validator = new CreateTaskValidator();

        var request = new CreateTask(
            Guid.NewGuid(),
            "Title",
            "Valid Description",
            "Todo",
            DateTime.Today
        );

        var handler = new CreateTaskHandler(mockRepo.Object, validator);
        var result = await handler.Handle(request, CancellationToken.None);
        
        Assert.Equal("Title", result);

        mockRepo.Verify(r => r.Create(
            request.id,
            request.title,
            request.description,
            request.status,
            request.date
        ), Times.Once);
    }

    [Fact]
    public async Task InvalidUpdate()
    {
        var mockRepo = new Mock<ITaskRepository>();
        var validator = new UpdateTaskValidator();

        var request = new UpdateTask(Guid.NewGuid(), "", "fd", "Donr", DateTime.Today);
        
        var handler = new UpdateTaskHandler(mockRepo.Object, validator);
        
        await Assert.ThrowsAsync<Exception>(async () =>
            await handler.Handle(request, CancellationToken.None)
        );
        
        mockRepo.Verify(r => r.Change(
            It.IsAny<Guid>(),
            It.IsAny<string>(),
            It.IsAny<string>(),
            It.IsAny<string>(),
            It.IsAny<DateTime>()
        ), Times.Never);
    }

    [Fact]
    public async Task ValidUpdate()
    {
        var mockRepo = new Mock<ITaskRepository>();
        var validator = new UpdateTaskValidator();

        var request = new UpdateTask(
            Guid.NewGuid(),
            "Title",
            "Valid Description",
            "Todo",
            DateTime.Today
        );

        var handler = new UpdateTaskHandler(mockRepo.Object, validator);
        var result = await handler.Handle(request, CancellationToken.None);
        
        Assert.Equal("Title", result);
        mockRepo.Verify(r => r.Change(
            It.IsAny<Guid>(),
            It.IsAny<string>(),
            It.IsAny<string>(),
            It.IsAny<string>(),
            It.IsAny<DateTime>()
            ), Times.Once);
    }

    [Fact]
    public async Task DeleteTask()
    {
        var mockRepo = new Mock<ITaskRepository>();
        var validator = new CreateTaskValidator();

        var request = new CreateTask(
            Guid.NewGuid(),
            "Title",
            "Valid Description",
            "Todo",
            DateTime.Today
        );

        var handler = new CreateTaskHandler(mockRepo.Object, validator);
        await handler.Handle(request, CancellationToken.None);

        var id = request.id;

        var deleteRequest = new DeleteTask(id);
        var deleteHandler = new DeleteTaskHandler(mockRepo.Object);

        var deletedId = await deleteHandler.Handle(deleteRequest, CancellationToken.None);
        
        Assert.Equal(id, deletedId);
        
        mockRepo.Verify(r => r.Delete(It.IsAny<Guid>()), Times.Once);
    }
}
