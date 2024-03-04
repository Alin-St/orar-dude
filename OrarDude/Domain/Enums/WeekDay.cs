using System.Diagnostics;

namespace OrarDude.Domain.Enums;

static class WeekDay
{
    public static readonly string Luni = "Luni";
    public static readonly string Marti = "Marti";
    public static readonly string Miercuri = "Miercuri";
    public static readonly string Joi = "Joi";
    public static readonly string Vineri = "Vineri";

    public static string[] GetValues()
        => new[] { Luni, Marti, Miercuri, Joi, Vineri };

    public static void AssertValue(string ziua)
        => Trace.Assert(GetValues().Contains(ziua));
}
