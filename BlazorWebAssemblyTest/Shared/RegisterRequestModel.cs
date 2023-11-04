
namespace BlazorWebAssemblyTest.Shared;
public class RegisterRequestModel
{
    public string? Name { get; set; } = null;
    public Gender? Gender { get; set; } = null;
    public string? PhoneNumber { get; set; } = null;
    public string? University { get; set; } = null;
    public string? Department { get; set; } = null;
    public List<string>? Interests { get; set; } = new();
}