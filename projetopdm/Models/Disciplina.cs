using SQLite;

namespace projetopdm.Models;

public class Disciplina
{
    [PrimaryKey, AutoIncrement]
    public int DisId { get; set; }

    [MaxLength(50)]
    public string DisNome { get; set; } = string.Empty;

    [MaxLength(25)]
    public string DisSigla { get; set; } = string.Empty;

    [MaxLength(500)]
    public string DisObservacoes { get; set; } = string.Empty;

    public int CurId { get; set; }
}