namespace OrarDude.Domain;

class InputPage
{
    public string PageTitle { get; }
    public IReadOnlyList<InputTimetable> Timetables { get; }

    public InputPage(string pageTitle, IEnumerable<InputTimetable> timetables)
    {
        PageTitle = pageTitle;
        Timetables = timetables.ToList();
    }
}
