using System.Diagnostics;

namespace OrarDude.Domain.Enums;

static class ClassHours
{
    public static readonly string _8_10 = "8-10";
    public static readonly string _10_12 = "10-12";
    public static readonly string _12_14 = "12-14";
    public static readonly string _14_16 = "14-16";
    public static readonly string _16_18 = "16-18";
    public static readonly string _18_20 = "18-20";

    public static string[] GetValues()
        => new[] { _8_10, _10_12, _12_14, _14_16, _16_18, _18_20 };

    public static void AssertValue(string orele)
        => Trace.Assert(GetValues().Contains(orele));
}
