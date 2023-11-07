using BlazorWebAssemblyTest.Server.Managers;
using Microsoft.AspNetCore.Mvc;

namespace BlazorWebAssemblyTest.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GroupsController : ControllerBase
{
    private PersonManager _personManager;

    public GroupsController(PersonManager personManager)
    {
        _personManager = personManager;
    }

    [HttpGet("getMembers")]
    public IActionResult GetMembers(string? seatId)
    {
        var members = _personManager.GetGroupMembers(seatId);
        if (members == null)
            return NotFound();

        return Ok(members);
    }

    [HttpGet("group")]
    public IActionResult Group()
    {
        _personManager.GroupClients();
        return Ok();
    }
}
