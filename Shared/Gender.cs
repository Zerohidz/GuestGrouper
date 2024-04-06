using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuestGrouper.Shared;
public enum Gender
{
    Male,
    Female
}

public static class GenderExtensions
{
    public static string GetName(this Gender gender)
    {
        return gender switch
        {
            Gender.Male => "Erkek",
            Gender.Female => "Kadın",
            _ => "",
        };
    }
}