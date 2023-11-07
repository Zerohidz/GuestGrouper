using BlazorWebAssemblyTest.Server.Entities;
using BlazorWebAssemblyTest.Shared;

namespace BlazorWebAssemblyTest.Server.Managers;

// TODO: SeatId

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
        AddPerson(new() { SeatId= "1", Name = "Erkek0", Gender = Gender.Male, University = "a", Department = "d", Interests = new string[] { "a", "b", "t" } });
        AddPerson(new() { SeatId= "2", Name = "Erkek1", Gender = Gender.Male, University = "a", Department = "d", Interests = new string[] { "a", "c", "r" } });
        AddPerson(new() { SeatId= "3", Name = "Erkek2", Gender = Gender.Male, University = "b", Department = "e", Interests = new string[] { "a", "b", "r" } });
        AddPerson(new() { SeatId= "4", Name = "Erkek3", Gender = Gender.Male, University = "b", Department = "f", Interests = new string[] { "c", "s", "t" } });
        AddPerson(new() { SeatId= "5", Name = "Erkek4", Gender = Gender.Male, University = "a", Department = "f", Interests = new string[] { "a", "s", "r" } });

        AddPerson(new() { SeatId= "6", Name = "Kadýn5", Gender = Gender.Female, University = "c", Department = "f", Interests = new string[] { "a", "s", "c" } });
        AddPerson(new() { SeatId= "7", Name = "Kadýn2", Gender = Gender.Female, University = "b", Department = "e", Interests = new string[] { "a", "b", "r" } });
        AddPerson(new() { SeatId= "8", Name = "Kadýn3", Gender = Gender.Female, University = "b", Department = "f", Interests = new string[] { "c", "s", "t" } });
        AddPerson(new() { SeatId= "9", Name = "Kadýn1", Gender = Gender.Female, University = "a", Department = "d", Interests = new string[] { "t", "a", "r" } });
        AddPerson(new() { SeatId= "10", Name = "Kadýn0", Gender = Gender.Female, University = "a", Department = "d", Interests = new string[] { "a", "b", "s" } });
        AddPerson(new() { SeatId= "11", Name = "Kadýn4", Gender = Gender.Female, University = "a", Department = "f", Interests = new string[] { "c", "s", "r" } });

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
        for (int i = 0; i < 4; i++)
        {
            if (similarities.Count < 1)
                break;

            if (i == 3 && similarities.Count != 1)
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

    internal GroupMemberDto[] GetGroupMembers(string? seatId)
    {
        if (!_isGrouped)
            return null;

        List<Person> allClients = new(_maleClients);
        allClients.AddRange(_femaleClients);

        foreach (Person client in allClients)
        {
            if (client.SeatId == seatId)
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

    internal void ClearData()
    {
        _maleClients.Clear();
        _femaleClients.Clear();
    }
}
