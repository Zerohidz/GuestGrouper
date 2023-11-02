using BlazorWebAssemblyTest.Shared;

namespace BlazorWebAssemblyTest.Server.Entities;

public record Person(
    Person[]? GroupMembers = null,
    string? Name = null,
    string? PhoneNumber = null,
    string? University = null,
    string? Department = null,
    string[]? Interests = null
)
{
    public static implicit operator Person(RegisterRequestModel model)
    {
        return new Person()
        {
            Name = model.Name,
            PhoneNumber = model.PhoneNumber,
            Department = model.Department,
            Interests = model.Interests?.ToArray(),
            University = model.University
        };
    }
}
