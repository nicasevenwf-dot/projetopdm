using SQLite;
using projetopdm.Models;

namespace projetopdm.Data;

public class Database
{
    private readonly SQLiteAsyncConnection _database;
    private static bool _inicializado = false;

    public Database()
    {
        string caminhoBanco = Path.Combine(
            FileSystem.AppDataDirectory,
            "academico.db3"
        );

        _database = new SQLiteAsyncConnection(caminhoBanco);
    }

    public async Task Inicializar()
    {
        if (_inicializado) return;

        await _database.CreateTableAsync<Periodo>();
        await _database.CreateTableAsync<Curso>();
        await _database.CreateTableAsync<Disciplina>();

        _inicializado = true;
    }

    // ===================== PERÍODO =====================
    public async Task<int> InserirPeriodo(Periodo periodo)
    {
        await Inicializar();
        return await _database.InsertAsync(periodo);
    }

    public async Task<List<Periodo>> ListarPeriodos()
    {
        await Inicializar();
        return await _database.Table<Periodo>().ToListAsync();
    }

    public async Task<List<Periodo>> PesquisarPeriodos(string? texto)
    {
        await Inicializar();
        texto ??= string.Empty;

        return await _database.Table<Periodo>()
            .Where(p => p.PerNome.Contains(texto) || p.PerSigla.Contains(texto))
            .ToListAsync();
    }

    public async Task<int> AlterarPeriodo(Periodo periodo)
    {
        await Inicializar();
        return await _database.UpdateAsync(periodo);
    }

    // Retorna 0 se não excluiu (ainda há cursos vinculados)
    public async Task<int> ExcluirPeriodo(Periodo periodo)
    {
        await Inicializar();

        int vinculos = await _database.Table<Curso>()
            .Where(c => c.PerId == periodo.PerId)
            .CountAsync();

        if (vinculos > 0) return 0;

        return await _database.DeleteAsync(periodo);
    }

    // ===================== CURSO =====================
    public async Task<int> InserirCurso(Curso curso)
    {
        await Inicializar();
        return await _database.InsertAsync(curso);
    }

    public async Task<List<Curso>> ListarCursos()
    {
        await Inicializar();
        return await _database.Table<Curso>().ToListAsync();
    }

    public async Task<List<Curso>> PesquisarCursos(string? texto)
    {
        await Inicializar();
        texto ??= string.Empty;

        return await _database.Table<Curso>()
            .Where(c => c.CurNome.Contains(texto) || c.CurSigla.Contains(texto))
            .ToListAsync();
    }

    public async Task<int> AlterarCurso(Curso curso)
    {
        await Inicializar();
        return await _database.UpdateAsync(curso);
    }

    // Retorna 0 se não excluiu (ainda há disciplinas vinculadas)
    public async Task<int> ExcluirCurso(Curso curso)
    {
        await Inicializar();

        int vinculos = await _database.Table<Disciplina>()
            .Where(d => d.CurId == curso.CurId)
            .CountAsync();

        if (vinculos > 0) return 0;

        return await _database.DeleteAsync(curso);
    }

    // ===================== DISCIPLINA =====================
    public async Task<int> InserirDisciplina(Disciplina disciplina)
    {
        await Inicializar();
        return await _database.InsertAsync(disciplina);
    }

    public async Task<List<Disciplina>> ListarDisciplinas()
    {
        await Inicializar();
        return await _database.Table<Disciplina>().ToListAsync();
    }

    public async Task<List<Disciplina>> PesquisarDisciplinas(string? texto)
    {
        await Inicializar();
        texto ??= string.Empty;

        return await _database.Table<Disciplina>()
            .Where(d => d.DisNome.Contains(texto) || d.DisSigla.Contains(texto))
            .ToListAsync();
    }

    public async Task<int> AlterarDisciplina(Disciplina disciplina)
    {
        await Inicializar();
        return await _database.UpdateAsync(disciplina);
    }

    public async Task<int> ExcluirDisciplina(Disciplina disciplina)
    {
        await Inicializar();
        return await _database.DeleteAsync(disciplina);
    }
}