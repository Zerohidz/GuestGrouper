using BlazorWebAssemblyTest.Server.Managers;
using BlazorWebAssemblyTest.Shared;
using Microsoft.AspNetCore.Mvc;

namespace BlazorWebAssemblyTest.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientsController : ControllerBase
{
    private PersonManager _personManager;

    public ClientsController(PersonManager personManager)
    {
        _personManager = personManager;
    }

    [HttpGet("getMembers")]
    public IActionResult GetMembers(string? seatId)
    {
        GroupMemberDto[]? members = null;
        members = _personManager.GetGroupMembers(seatId);

        if (members == null)
            return NotFound();

        return Ok(members);
    }

    [HttpGet("getAllClients")]
    public IActionResult GetAllClients()
    {
        RegisterRequestModel[]? members = null;
        members = _personManager.GetAllClients();

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

    [HttpGet("clearData")]
    public IActionResult ClearData()
    {
        _personManager.ClearData();
        return Ok();
    }
}
