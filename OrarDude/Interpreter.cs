using OrarDude.Domain;
using System.Diagnostics;

namespace OrarDude;

class Interpreter
{
    static readonly string timetableTitle = "Grupa 936";

    static readonly string[] courseWhitelist =
    {
        "Verificarea si validarea sistemelor soft",
        "Calcul numeric",
        "Elaborarea lucrarii de licenta",
        "Tehnici de testare software",
        "Android Things",
        "Istoria informaticii",
    };

    static readonly string[] courseBlacklist =
    {
        "Administrarea de sistem si de retea",
        "Blockchain: Smart contracts",
        "Business Intelligence",
        "Design patterns",
        "Dezvoltarea de jocuri",
        "Etica si integritate academica (in informatica)",
        "Gestiunea proiectelor soft",
        "Grafica pe calculator",
        "Instrumentatie virtuala",
        "Introducere in prelucrarea limbajului natural",
        "Istoria matematicii",
        "Managementul clasei de elevi",
        "Modele de inteligenta artificiala in schimbarea climatica",
        "Tehnici de calcul cuantice cu aplicatii in criptografie si IA",
        "Tehnici de realizare a sistemelor inteligente",
    };

    static readonly string blacklistedFormation = "936/1";

    static readonly (string disciplina, string pageTitle, string timetableTitle, string ignoreFormation)[] recontractari =
    {
        ("Medii de proiectare si programare", "Orar Anul 2 Informatica - in limba engleza", "Grupa 926", "926/2"),
    };

    public static Output ProcessData(InputPage input, IReadOnlyList<InputPage> extraPages)
    {
        // Check config data to be sure. Check if course whitelist and blacklist are disjunct sets.
        string[] cwl = courseWhitelist, cbl = courseBlacklist;
        Trace.Assert(cwl.All(c => cwl.Count(x => x == c) == 1 && !cbl.Contains(c)));
        Trace.Assert(cbl.All(c => !cwl.Contains(c) && cbl.Count(x => x == c) == 1));

        // Check input data structure
        Trace.Assert(input.Timetables.Count(tt => tt.TableTitle == timetableTitle) == 1);
        var inputTimetable = input.Timetables.First(tt => tt.TableTitle == timetableTitle);

        // Build output
        var sections = new List<(string ziua, List<OutputRow> rows)>();
        string? lastZiua = null;

        foreach (var inputRow in inputTimetable.Rows)
        {
            if (inputRow.Ziua != lastZiua)
                sections.Add((inputRow.Ziua, new()));
            lastZiua = inputRow.Ziua;

            // Check for red background
            Trace.Assert(courseWhitelist.Contains(inputRow.Disciplina) || courseBlacklist.Contains(inputRow.Disciplina));
            bool redBackground = courseBlacklist.Contains(inputRow.Disciplina) || inputRow.Formatia == blacklistedFormation;

            var outputRow = new OutputRow(inputRow.Orele,
                                          inputRow.Frecventa,
                                          inputRow.Sala,
                                          inputRow.Formatia,
                                          inputRow.Tipul,
                                          inputRow.Disciplina,
                                          inputRow.CadrulDidactic,
                                          redBackground);
            sections[^1].rows.Add(outputRow);
        }

        // Add recontractari
        foreach (var rec in recontractari)
        {
            var recPage = extraPages.First(p => p.PageTitle == rec.pageTitle);
            Trace.Assert(recPage is not null);

            var recTimetable = recPage!.Timetables.First(tt => tt.TableTitle == rec.timetableTitle);
            Trace.Assert(recTimetable is not null);
        }

        // Return output
        string outputTitle = input.PageTitle + " | " + timetableTitle;
        var outputSections = sections.Select(s => new OutputSection(s.ziua, s.rows)).ToList();
        var output = new Output(outputTitle, outputSections);
        return output;
    }
}
