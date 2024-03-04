using System.Diagnostics;

namespace OrarDude.Domain.Enums;

static class FrequencyType
{
    public static string None { get; } = "&nbsp;";
    public static string Sapt1 { get; } = "sapt. 1";
    public static string Sapt2 { get; } = "sapt. 2";

    public static string[] GetValues()
        => new[] { None, Sapt1, Sapt2 };

    public static void AssertValue(string frecventa)
        => Trace.Assert(GetValues().Contains(frecventa));
}
