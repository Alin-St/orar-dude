using System.Diagnostics;

namespace OrarDude.Domain.Enums;

static class ClassType
{
    public static readonly string Curs = "Curs";
    public static readonly string Seminar = "Seminar";
    public static readonly string Laborator = "Laborator";

    public static string[] GetValues()
        => new[] { Curs, Seminar, Laborator };

    public static void AssertValue(string tipul)
        => Trace.Assert(GetValues().Contains(tipul));
}
