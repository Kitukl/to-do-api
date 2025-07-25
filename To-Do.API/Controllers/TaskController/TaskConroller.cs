using BLL.Commands.Task.Create;
using BLL.Commands.Task.Delete;
using BLL.Commands.Task.Update;
using BLL.DTOs;
using BLL.Query.Task.GetAllTasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace To_Do.Controllers.TaskController;

[Route("api/[controller]")]
[ApiController]
public class TaskController : ControllerBase
{
    private readonly ISender _sender;

    public TaskController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    public async Task<ActionResult<string>> CreateTask(CreateTask command)
    {
        var title = await _sender.Send(command);
        return Ok(title);
    }

    [HttpGet]
    public async Task<ActionResult<List<TaskDTOs>>> GetAll()
    {
        var tasks = await _sender.Send(new GetAllTask());
        return Ok(tasks);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<Guid>> Delete(Guid id)
    {
        var command = new DeleteTask(id);
        var res_id = await _sender.Send(command);
        return Ok(res_id);
    }
    
    [HttpPut("{id}")]
    public async Task<ActionResult<Guid>> Update(Guid id, [FromBody] UpdateTaskDTO taskDto)
    { 
        var command = new UpdateTask(id, taskDto.Title, taskDto.Description, taskDto.Status, taskDto.Date);
        var title = await _sender.Send(command);
        return Ok(title);
    }
}