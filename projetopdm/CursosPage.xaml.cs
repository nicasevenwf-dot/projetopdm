using projetopdm.Data;
using projetopdm.Models;

namespace projetopdm;

public partial class CursosPage : ContentPage
{
    private readonly Database _database;

    public CursosPage()
    {
        InitializeComponent();

        _database = new Database();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        await CarregarCursos();
    }

    private async void OnPesquisarTextChanged(object sender, EventArgs e)
    {
        await CarregarCursos();
    }

    private async Task CarregarCursos()
    {
        string texto = searchBar.Text ?? string.Empty;

        List<Curso> cursos = string.IsNullOrWhiteSpace(texto)
            ? await _database.ListarCursos()
            : await _database.PesquisarCursos(texto);

        listaCursos.ItemsSource = cursos;
    }

    private async void AbrirInserirCurso(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("InsCursoPage");
    }

    private async void AbrirAlterarCurso(object sender, EventArgs e)
    {
        Button botao = (Button)sender;

        Curso curso = (Curso)botao.CommandParameter;

        await Shell.Current.GoToAsync(
            "AltCursoPage",
            new Dictionary<string, object>
            {
                { "Curso", curso }
            }
        );
    }

    private async void ExcluirCurso(object sender, EventArgs e)
    {
        Button botao = (Button)sender;

        Curso curso = (Curso)botao.CommandParameter;

        bool confirmar = await popup.MostrarAsync(
            "Excluir curso",
            $"Deseja realmente excluir {curso.CurNome}?",
            "Excluir",
            "Cancelar"
        );

        if (!confirmar)
            return;

        int resultado = await _database.ExcluirCurso(curso);

        if (resultado == 0)
        {
            await popup.MostrarAsync(
                "Atenção",
                "Não é possível excluir: existem disciplinas vinculadas a este curso.",
                "OK",
                null,
                "⚠️",
                Colors.DarkOrange
            );

            return;
        }

        await CarregarCursos();

        await popup.MostrarAsync(
            "Sucesso",
            "Curso excluído com sucesso!",
            "OK",
            null,
            "✅",
            Colors.Green
        );
    }
}