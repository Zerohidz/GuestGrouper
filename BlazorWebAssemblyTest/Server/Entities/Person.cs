using BlazorWebAssemblyTest.Shared;

namespace BlazorWebAssemblyTest.Server.Entities;

public record Person(
    List<Person>? GroupMembers = null,
    string? Name = null,
    Gender? Gender = null,
    string? PhoneNumber = null,
    string? University = null,
    string? Department = null,
    string[]? Interests = null
)
{
    public Person() : this(new(), null, null, null, null, null, null)
    {
    }

    public static implicit operator Person(RegisterRequestModel model)
    {
        return new Person()
        {
            GroupMembers = new(),
            Name = model.Name,
            Gender = model.Gender,
            PhoneNumber = model.PhoneNumber,
            Department = model.Department,
            Interests = model.Interests?.ToArray(),
            University = model.University
        };
    }
}
