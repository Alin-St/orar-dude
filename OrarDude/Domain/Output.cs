namespace OrarDude.Domain;

class Output
{
    public string Title { get; }
    public IReadOnlyList<OutputSection> Sections { get; }

    public Output(string title, IEnumerable<OutputSection> sections)
    {
        Title = title;
        Sections = sections.ToList();
    }
}
