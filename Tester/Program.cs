using BlazorWebAssemblyTest.Server.Managers;
using BlazorWebAssemblyTest.Shared;

namespace Tester;

internal class Program
{
    static void Main(string[] args)
    {
        PersonManager manager = new();

        manager.AddPerson(new() { Name = "Erkek0", Gender = Gender.Male, University = "a", Department = "d", Interests = new string[] { "a", "b", "t" } });
        manager.AddPerson(new() { Name = "Erkek1", Gender = Gender.Male, University = "a", Department = "d", Interests = new string[] { "a", "c", "r" } });
        manager.AddPerson(new() { Name = "Erkek2", Gender = Gender.Male, University = "b", Department = "e", Interests = new string[] { "a", "b", "r" } });
        manager.AddPerson(new() { Name = "Erkek3", Gender = Gender.Male, University = "b", Department = "f", Interests = new string[] { "c", "s", "t" } });
        manager.AddPerson(new() { Name = "Erkek4", Gender = Gender.Male, University = "a", Department = "f", Interests = new string[] { "a", "s", "r" } });
        //manager.AddPerson(new() { Name = "Erkek5", Gender = Gender.Male, University = "c", Department = "f", Interests = new string[] { "s", "s", "t" } });

        manager.AddPerson(new() { Name = "Kadın5", Gender = Gender.Female, University = "c", Department = "f", Interests = new string[] { "a", "s", "c" } });
        manager.AddPerson(new() { Name = "Kadın2", Gender = Gender.Female, University = "b", Department = "e", Interests = new string[] { "a", "b", "r" } });
        manager.AddPerson(new() { Name = "Kadın3", Gender = Gender.Female, University = "b", Department = "f", Interests = new string[] { "c", "s", "t" } });
        manager.AddPerson(new() { Name = "Kadın1", Gender = Gender.Female, University = "a", Department = "d", Interests = new string[] { "t", "a", "r" } });
        manager.AddPerson(new() { Name = "Kadın0", Gender = Gender.Female, University = "a", Department = "d", Interests = new string[] { "a", "b", "s" } });
        manager.AddPerson(new() { Name = "Kadın4", Gender = Gender.Female, University = "a", Department = "f", Interests = new string[] { "c", "s", "r" } });

        manager.GroupClients();
    }
}
