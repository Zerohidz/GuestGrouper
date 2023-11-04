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
    public IActionResult GetMembers(string name)
    {
        var members = _personManager.GetGroupMembers(name);
        if (members == null)
            return NotFound();

        Console.WriteLine(members.Length);
        return Ok(members);
    }
}
