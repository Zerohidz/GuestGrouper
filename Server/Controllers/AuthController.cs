using GuestGrouper.Server.Managers;
using GuestGrouper.Shared;
using Microsoft.AspNetCore.Mvc;

namespace GuestGrouper.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private ClientManager _clientManager;

    public AuthController(ClientManager clientManager)
    {
        _clientManager = clientManager;
    }

    [HttpPost("register")]
    public IActionResult Register(RegisterRequestModel model)
    {
        Console.WriteLine($"{model.Name} registered!");
        _clientManager.AddPerson(model);
        return Ok();
    }
}