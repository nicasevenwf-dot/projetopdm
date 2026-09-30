using SQLite;

namespace projetopdm.Models;

public class Curso
{
    [PrimaryKey, AutoIncrement]
    public int CurId { get; set; }

    [MaxLength(50)]
    public string CurNome { get; set; } = string.Empty;

    [MaxLength(25)]
    public string CurSigla { get; set; } = string.Empty;

    [MaxLength(500)]
    public string CurObservacoes { get; set; } = string.Empty;

    public int PerId { get; set; }
}