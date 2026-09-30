using SQLite;
using projetopdm.Models;

namespace projetopdm.Data;

public class Database
{
    private readonly SQLiteAsyncConnection _database;

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
        await _database.CreateTableAsync<Periodo>();
        await _database.CreateTableAsync<Curso>();
        await _database.CreateTableAsync<Disciplina>();
    }
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
    public async Task<int> ExcluirPeriodo(Periodo periodo)
    {
        await Inicializar();

        return await _database.DeleteAsync(periodo);
    }
    public async Task<int> AlterarPeriodo(Periodo periodo)
    {
        await Inicializar();

        return await _database.UpdateAsync(periodo);
    }




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

    public async Task<int> AlterarCurso(Curso curso)
    {
        await Inicializar();

        return await _database.UpdateAsync(curso);
    }

    public async Task<int> ExcluirCurso(Curso curso)
    {
        await Inicializar();

        return await _database.DeleteAsync(curso);
    }


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