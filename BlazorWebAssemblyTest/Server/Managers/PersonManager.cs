using BlazorWebAssemblyTest.Server.Entities;
using BlazorWebAssemblyTest.Shared;

namespace BlazorWebAssemblyTest.Server.Managers;

public class PersonManager
{
    private readonly List<Person> _maleClients = new();
    private readonly List<Person> _femaleClients = new();
    private bool _isGrouped = false;

    public void AddPerson(Person person)
    {
        if (person.Gender == Gender.Male)
            _maleClients.Add(person);
        else if (person.Gender == Gender.Female)
            _femaleClients.Add(person);
    }

    public void GroupClients()
    {
        GroupClients(_maleClients);
        GroupClients(_femaleClients);

        _isGrouped = true;
    }

    private void GroupClients(List<Person> clientList)
    {
        if (clientList.Count < 1)
        {
            Console.WriteLine("Listede adam yok");
            return;
        }

        List<Person> ungrouped = new(clientList);

        while (ungrouped.Count > 0)
        {
            Person groupLeader = ungrouped[0]; // Pick the first ungrouped person as the leader of a group
            ungrouped.RemoveAt(0);

            FindGroupMembers(ungrouped, groupLeader);
        }

        // Now each client knows its group members

    }

    private void FindGroupMembers(List<Person> ungrouped, Person groupLeader)
    {
        List<Person> newGroup = new();
        newGroup.Add(groupLeader);

        List<(Person Person, int Similarity)> similarities = new();
        for (int i = 0; i < ungrouped.Count; i++)
        {
            Person person = ungrouped[i];
            similarities.Add((person, GetSimilarityAmout(groupLeader, person)));
        }

        Random random = new Random();
        similarities = similarities.OrderBy(p => random.Next()).ToList();
        similarities = similarities.OrderByDescending(p => p.Similarity).ToList();
        for (int i = 0; i < 3; i++)
        {
            if (similarities.Count < 1)
                break;

            Person person = similarities[0].Person;
            newGroup.Add(person);
            ungrouped.Remove(person);
            similarities.RemoveAt(0);
        }

        Console.WriteLine("Group ---------------");
        foreach (Person client in newGroup)
        {   
            client.GroupMembers!.Clear();
            foreach (Person otherClient in newGroup)
            {
                if (client == otherClient)
                    continue;
                client.GroupMembers.Add(otherClient);
            }

            Console.WriteLine(client.Name);
        }
        Console.WriteLine();
    }

    private static int GetSimilarityAmout(Person person1, Person person2)
    {
        int similarity = 0;

        if (person1.University == person2.University)
            similarity += 5;
        if (person1.Department == person2.Department)
            similarity += 2;
        foreach (string interest in person1.Interests!)
        {
            if (person2.Interests!.Contains(interest))
                similarity += 7;
        }

        Console.WriteLine($"{person1.Name} and {person2.Name} are {similarity} similar.");

        return similarity;
    }

    internal GroupMemberDto[] GetGroupMembers(string name)
    {
        if (!_isGrouped)
            return null;

        List<Person> allClients = new(_maleClients);
        allClients.AddRange(_femaleClients);

        foreach (Person client in allClients)
        {
            if (client.Name == name)
            {
                return client.GroupMembers!.Select(p => new GroupMemberDto
                {
                    SeatId = p.SeatId,
                    Name = p.Name,
                    University = p.University,
                    Department = p.Department
                }).ToArray();
            }
        }

        return null;
    }
}
