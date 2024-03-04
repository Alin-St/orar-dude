namespace OrarDude.Domain;

class OutputRow
{
    public string Orele { get; }
    public string Frecventa { get; }
    public string Sala { get; }
    public string Formatia { get; }
    public string Tipul { get; }
    public string Disciplina { get; }
    public string CadrulDidactic { get; }
    public bool RedBackground { get; }

    public OutputRow(string orele,
                     string frecventa,
                     string sala,
                     string formatia,
                     string tipul,
                     string disciplina,
                     string cadrulDidactic,
                     bool redBackground)
    {
        Orele = orele;
        Frecventa = frecventa;
        Sala = sala;
        Formatia = formatia;
        Tipul = tipul;
        Disciplina = disciplina;
        CadrulDidactic = cadrulDidactic;
        RedBackground = redBackground;
    }
}
