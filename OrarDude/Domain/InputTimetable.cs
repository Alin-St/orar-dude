namespace OrarDude.Domain;

class InputTimetable
{
    public string TableTitle { get; }
    public IReadOnlyList<InputRow> Rows { get; }

    public InputTimetable(string tableTitle, IEnumerable<InputRow> rows)
    {
        TableTitle = tableTitle;
        Rows = rows.ToList();
    }
}
