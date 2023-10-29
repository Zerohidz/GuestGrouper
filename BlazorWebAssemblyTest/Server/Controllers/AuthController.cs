using BlazorWebAssemblyTest.Server.Managers;
using BlazorWebAssemblyTest.Shared;
using Microsoft.AspNetCore.Mvc;

namespace BlazorWebAssemblyTest.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private PersonManager _personManager;

    public AuthController(PersonManager personManager)
    {
        _personManager = personManager;
    }

    [HttpPost("register")]
    public IActionResult Register(RegisterRequestModel model)
    {
        Console.WriteLine($"{model.Name} registered!");
        _personManager.AddPerson(model);
        return Ok();
    }
}