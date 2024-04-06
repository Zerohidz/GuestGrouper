using GuestGrouper.Shared;

namespace GuestGrouper.Server.Entities;

public record Person(
    List<Person>? GroupMembers = null,
    string? SeatId = null,
    string? Name = null,
    Gender? Gender = null,
    string? PhoneNumber = null,
    string? University = null,
    string? Department = null,
    string[]? Interests = null
)
{
    public Person() : this(new(), null, null, null, null, null, null, new string[0])
    {
    }

    public static implicit operator Person(RegisterRequestModel model)
    {
        return new Person()
        {
            GroupMembers = new(),
            SeatId = model.SeatId,
            Name = model.Name,
            Gender = model.Gender,
            PhoneNumber = model.PhoneNumber,
            Department = model.Department,
            Interests = model.Interests?.ToArray(),
            University = model.University
        };
    }
}
