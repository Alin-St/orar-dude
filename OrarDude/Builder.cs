using HtmlAgilityPack;
using OrarDude.Domain;
using OrarDude.Domain.Enums;

namespace OrarDude;

class Builder
{
    public static string BuildHtml(Output timetable)
    {
        string startHtml =
            $"<html>" +
            $"<head><style>" +
            $"table,th,td{{border:1px solid black;}}" +
            $"</style></head>" +
            $"<body><center>" +
            $"<h1>{timetable.Title}</h1><table><tr/></table>" +
            $"</center></body>" +
            $"</html>";
        var doc = new HtmlDocument();
        doc.LoadHtml(startHtml);

        var tableNode = doc.DocumentNode.SelectSingleNode("//table");

        // Add table heads
        var headerRowNode = tableNode.Element("tr");
        foreach (var ch in Parser.ExpectedHeads)
            headerRowNode.ChildNodes.Add(HtmlNode.CreateNode($"<th>{ch}</th>"));

        // Add table data
        foreach (var section in timetable.Sections)
        {
            foreach (var row in section.Rows)
            {
                tableNode.ChildNodes.Add(HtmlNode.CreateNode("<tr>"));
                var rowNode = tableNode.ChildNodes[^1];

                string[] props = new[]
                {
                    section.Ziua,
                    row.Orele,
                    row.Frecventa,
                    row.Sala,
                    row.Formatia,
                    row.Tipul,
                    row.Disciplina,
                    row.CadrulDidactic,
                };
                for (int i = 0; i < props.Length; i++)
                {
                    string? prop = props[i];
                    rowNode.ChildNodes.Add(HtmlNode.CreateNode($"<td>{prop}</td>"));

                    if (!row.RedBackground)
                    {
                        if (i == 2 && (prop == FrequencyType.Sapt1 || prop == FrequencyType.Sapt2)) // frecventa
                            rowNode.ChildNodes[^1].Attributes.Add("style", "background-color: green;");

                        if (i == 5 && (prop == ClassType.Curs)) // curs
                            rowNode.ChildNodes[^1].Attributes.Add("style", "background-color: blue;");
                    }
                }

                // Red background
                if (row.RedBackground)
                    rowNode.Attributes.Add("style", "background-color: red;");
            }
        }

        return doc.DocumentNode.OuterHtml;
    }
}
