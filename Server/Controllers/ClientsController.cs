using GuestGrouper.Server.Managers;
using GuestGrouper.Shared;
using Microsoft.AspNetCore.Mvc;

namespace GuestGrouper.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientsController : ControllerBase
{
    private ClientManager _clientManager;

    public ClientsController(ClientManager personManager)
    {
        _clientManager = personManager;
    }

    [HttpGet("getMembers")]
    public IActionResult GetMembers(string? seatId)
    {
        GroupMemberDto[]? members = null;
        members = _clientManager.GetGroupMembers(seatId);

        if (members == null)
            return NotFound();

        return Ok(members);
    }

    [HttpGet("getAllClients")]
    public IActionResult GetAllClients()
    {
        RegisterRequestModel[]? members = null;
        members = _clientManager.GetAllClients();

        if (members == null)
            return NotFound();

        return Ok(members);
    }


    [HttpGet("group")]
    public IActionResult Group()
    {
        _clientManager.GroupClients();
        return Ok();
    }

    [HttpGet("clearData")]
    public IActionResult ClearData()
    {
        _clientManager.ClearData();
        return Ok();
    }
}
