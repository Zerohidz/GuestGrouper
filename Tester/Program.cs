using BlazorWebAssemblyTest.Server.Managers;
using BlazorWebAssemblyTest.Shared;

namespace Tester;

internal class Program
{
    static void Main(string[] args)
    {
        PersonManager manager = new();

        manager.AddPerson(new() { Name = "Zerohidz0", Gender = Gender.Male });
        manager.AddPerson(new() { Name = "Zerohidz1", Gender = Gender.Male });
        manager.AddPerson(new() { Name = "Zerohidz2", Gender = Gender.Male });
        manager.AddPerson(new() { Name = "Zerohidz3", Gender = Gender.Male });
        manager.AddPerson(new() { Name = "Zerohidz4", Gender = Gender.Male });
        manager.AddPerson(new() { Name = "Zerohidz5", Gender = Gender.Male });

        manager.GroupClients();
    }
}
