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

    [HttpPost("create")]
    public async Task<ActionResult<string>> CreateTask(CreateTask command)
    {
        var title = await _sender.Send(command);
        return Ok(title);
    }

    [HttpGet("tasks")]
    public async Task<ActionResult<List<TaskDTOs>>> GetAll()
    {
        var tasks = await _sender.Send(new GetAllTask());
        return Ok(tasks);
    }

    [HttpDelete("delete")]
    public async Task<ActionResult<Guid>> Delete(DeleteTask command)
    {
        var id = await _sender.Send(command);
        return Ok(id);
    }
    
    [HttpPut("update")]
    public async Task<ActionResult<Guid>> Update(UpdateTask command)
    {
        var title = await _sender.Send(command);
        return Ok(title);
    }
}