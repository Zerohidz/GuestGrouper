using GuestGrouper.Server.Entities;
using GuestGrouper.Shared;

namespace GuestGrouper.Server.Managers;


public class ClientManager
{
    private readonly List<Person> _maleClients = new();
    private readonly List<Person> _femaleClients = new();
    private bool _isGrouped = false;

    public void AddPerson(Person person)
    {
        if (person.Gender == Gender.Male)
        {
            Person? duplicate = _maleClients.FirstOrDefault(c => c.Name == person.Name);
            if (duplicate != null)
                _maleClients.Remove(duplicate);

            _maleClients.Add(person);
        }
        else if (person.Gender == Gender.Female)
        {
            Person? duplicate = _femaleClients.FirstOrDefault(c => c.Name == person.Name);
            if (duplicate != null)
                _femaleClients.Remove(duplicate);
            _femaleClients.Add(person);
        }
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
            //Console.WriteLine("Listede adam yok");
            return;
        }

        List<Person> ungrouped = new(clientList);

        while (ungrouped.Count > 0)
        {
            Person groupLeader = ungrouped[0];
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

        //Console.WriteLine("Group ---------------");
        foreach (Person client in newGroup)
        {
            client.GroupMembers!.Clear();
            foreach (Person otherClient in newGroup)
            {
                //if (client == otherClient)
                //    continue;
                client.GroupMembers.Add(otherClient);
            }

            //Console.WriteLine(client.Name);
        }
        //Console.WriteLine();
    }

    private static int GetSimilarityAmout(Person person1, Person person2)
    {
        int similarity = 0;

        if (person1.University != person2.University)
            similarity += 5;
        if (person1.Department != person2.Department)
            similarity += 5;
        foreach (string interest in person1.Interests!)
        {
            if (person2.Interests!.Contains(interest))
                similarity += 7;
        }

        //Console.WriteLine($"{person1.Name} and {person2.Name} are {similarity} similar.");

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

        _isGrouped = false;
    }

    internal RegisterRequestModel[]? GetAllClients()
    {
        List<RegisterRequestModel> allClients = new();
        allClients.AddRange(_maleClients.Select(c => new RegisterRequestModel
        {
            Department = c.Department,
            Name = c.Name,
            Gender = c.Gender,
            Interests = c.Interests?.ToList(),
            University = c.University,
            PhoneNumber = c.PhoneNumber,
            SeatId = c.SeatId
        }));
        allClients.AddRange(_femaleClients.Select(c => new RegisterRequestModel
        {
            Department = c.Department,
            Name = c.Name,
            Gender = c.Gender,
            Interests = c.Interests?.ToList(),
            University = c.University,
            PhoneNumber = c.PhoneNumber,
            SeatId = c.SeatId
        }));

        return allClients.ToArray();
    }
}
