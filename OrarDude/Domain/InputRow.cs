using OrarDude.Domain.Enums;

namespace OrarDude.Domain;

class InputRow
{
    public string Ziua { get; }
    public string Orele { get; }
    public string Frecventa { get; }
    public string Sala { get; }
    public string SalaLink { get; }
    public string Formatia { get; }
    public string Tipul { get; }
    public string Disciplina { get; }
    public string DisciplinaLink { get; }
    public string CadrulDidactic { get; }
    public string CadrulDidacticLink { get; }

    public InputRow(string ziua,
                    string orele,
                    string frecventa,
                    string sala,
                    string salaLink,
                    string formatia,
                    string tipul,
                    string disciplina,
                    string disciplinaLink,
                    string cadrulDidactic,
                    string cadrulDidacticLink)
    {
        Ziua = ziua;
        Orele = orele;
        Frecventa = frecventa;
        Sala = sala;
        SalaLink = salaLink;
        Formatia = formatia;
        Tipul = tipul;
        Disciplina = disciplina;
        DisciplinaLink = disciplinaLink;
        CadrulDidactic = cadrulDidactic;
        CadrulDidacticLink = cadrulDidacticLink;
    }
}
