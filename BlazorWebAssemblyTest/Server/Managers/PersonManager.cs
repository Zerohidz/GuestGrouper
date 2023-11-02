using BlazorWebAssemblyTest.Server.Entities;
using BlazorWebAssemblyTest.Shared;

namespace BlazorWebAssemblyTest.Server.Managers;

public class PersonManager
{
    private readonly List<Person> _maleClients = new();
    private readonly List<Person> _femaleClients = new();

    public void AddPerson(Person person)
    {
        if (person.Gender == Gender.Male)
            _maleClients.Add(person);
        else if (person.Gender == Gender.Female)
            _femaleClients.Add(person);
    }

    public void MatchGroups()
    {
        // TODO: 4lü gruplar halinde match'le
    }
}
