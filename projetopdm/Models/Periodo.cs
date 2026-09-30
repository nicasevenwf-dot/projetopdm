using SQLite;

namespace projetopdm.Models;

public class Periodo
{
    [PrimaryKey, AutoIncrement]
    public int PerId { get; set; }

    [MaxLength(50)]
    public string PerNome { get; set; } = string.Empty;

    [MaxLength(5)]
    public string PerSigla { get; set; } = string.Empty;
}