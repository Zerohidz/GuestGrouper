using BlazorWebAssemblyTest.Server.Entities;

namespace BlazorWebAssemblyTest.Server.Managers;

public class PersonManager
{
    private readonly List<Person> _clients = new();

    public void AddPerson(Person person)
    {
        _clients.Add(person);
    }

    public void MatchGroups()
    {
        // TODO: 3lü gruplar halinde match'le
    }
}
