using HtmlAgilityPack;
using OrarDude.Domain;
using OrarDude.Domain.Enums;
using OrarDude.Utils;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace OrarDude;

static class Parser
{
    public static string[] ExpectedHeads { get; } =
    {
        "Ziua",
        "Orele",
        "Frecventa",
        "Sala",
        "Formatia",
        "Tipul",
        "Disciplina",
        "Cadrul didactic"
    };

    public static InputPage ParsePage(string pageHtml)
    {
        // Load html with Agility pack
        var doc = new HtmlDocument();
        doc.LoadHtml(pageHtml);

        // Get the center node
        var mainNode = doc.DocumentNode.SelectSingleNode("//center");
        var mainNodeChildren = mainNode.Elements().ToList();

        // Check document format: 1 page title + n * (1 table title + 1 table)
        Trace.Assert(mainNodeChildren.Count >= 3 && mainNodeChildren.Count % 2 == 1);
        for (int i = 0; i < mainNodeChildren.Count; i++)
        {
            var expectedName = (i == 0 || i % 2 == 1) ? "h1" : "table";
            Trace.Assert(mainNodeChildren[i].Name == expectedName);
        }

        // Build page
        string pageTitle = mainNodeChildren[0].InnerText;
        var timetables = new List<InputTimetable>();

        for (int i = 1; i < mainNodeChildren.Count; i += 2)
        {
            var timetable = ParseTimetable(mainNodeChildren[i], mainNodeChildren[i + 1]);
            timetables.Add(timetable);
        }

        var page = new InputPage(pageTitle, timetables);
        return page;
    }

    static InputTimetable ParseTimetable(HtmlNode titleNode, HtmlNode tableNode)
    {
        // Check node types
        Trace.Assert(titleNode.NodeType == HtmlNodeType.Element && titleNode.Name == "h1");
        Trace.Assert(tableNode.NodeType == HtmlNodeType.Element && tableNode.Name == "table");

        // Build result
        string tableTitle = titleNode.InnerText;
        var tableChildren = tableNode.Elements().ToList();

        // Check table children nodes
        Trace.Assert(tableChildren.Count >= 1); // table must have header row
        foreach (var rowNode in tableChildren)
            Trace.Assert(rowNode.Name == "tr");

        // Check header row
        var heads = new List<string>();
        foreach (var n in tableChildren[0].Elements())
        {
            Trace.Assert(n.Name == "th");
            heads.Add(n.InnerText.Trim());
        }
        Trace.Assert(heads.SequenceEqual(ExpectedHeads));

        // Add rows
        var rows = new List<InputRow>();
        foreach (var rowNode in tableChildren.Skip(1))
            rows.Add(ParseRow(rowNode));

        // Return result
        var result = new InputTimetable(tableTitle, rows);
        return result;
    }

    static InputRow ParseRow(HtmlNode rowNode)
    {
        // Check node structure
        Trace.Assert(rowNode.NodeType == HtmlNodeType.Element && rowNode.Name == "tr");

        var cellNodes = rowNode.Elements().ToList();
        Trace.Assert(cellNodes.Count == ExpectedHeads.Length);
        Trace.Assert(cellNodes.All(n => n.Name == "td"));

        // Build result
        string ziua = cellNodes[0].InnerText;
        WeekDay.AssertValue(ziua);

        string orele = cellNodes[1].InnerText;
        Trace.Assert(Regex.IsMatch(orele, @"\d{1,2}-\d{1,2}"));

        string frecventa = cellNodes[2].InnerText;
        FrequencyType.AssertValue(frecventa);

        string sala = cellNodes[3].InnerText;
        string salaLink = cellNodes[3].SelectSingleNode("a")?.Attributes["href"]?.Value!;
        Trace.Assert(!string.IsNullOrWhiteSpace(salaLink));

        string formatia = cellNodes[4].InnerText;

        string tipul = cellNodes[5].InnerText;
        ClassType.AssertValue(tipul);

        string disciplina = cellNodes[6].InnerText;
        string disciplinaLink = cellNodes[6].SelectSingleNode("a")?.Attributes["href"]?.Value!;
        Trace.Assert(!string.IsNullOrWhiteSpace(disciplinaLink));

        string cadrulDidactic = cellNodes[7].InnerText;
        string cadrulDidacticLink = cellNodes[7].SelectSingleNode("a")?.Attributes["href"]?.Value!;
        Trace.Assert(!string.IsNullOrWhiteSpace(cadrulDidacticLink));

        // Return result
        var result = new InputRow(ziua,
                                  orele,
                                  frecventa,
                                  sala,
                                  salaLink,
                                  formatia,
                                  tipul,
                                  disciplina,
                                  disciplinaLink,
                                  cadrulDidactic,
                                  cadrulDidacticLink);
        return result;
    }
}
