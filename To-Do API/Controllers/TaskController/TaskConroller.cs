using BLL.Commands.Task.Create;
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

    public async Task<ActionResult<string>> CreateTask(CreateTask command)
    {
        var title = await _sender.Send(command);
        return Ok(title);
    }
}