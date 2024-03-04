namespace OrarDude.Domain;

class OutputSection
{
    public string Ziua { get; }
    public IReadOnlyList<OutputRow> Rows { get; }

    public OutputSection(string ziua, IEnumerable<OutputRow> rows)
    {
        Ziua = ziua;
        Rows = rows.ToList();
    }
}
