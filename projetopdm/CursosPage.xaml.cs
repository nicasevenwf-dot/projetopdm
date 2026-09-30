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

    private async Task CarregarCursos()
    {
        List<Curso> cursos = await _database.ListarCursos();

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

        bool confirmar = await DisplayAlertAsync(
            "Excluir curso",
            $"Deseja realmente excluir {curso.CurNome}?",
            "Excluir",
            "Cancelar"
        );

        if (confirmar)
        {
            await _database.ExcluirCurso(curso);

            await CarregarCursos();

            await DisplayAlertAsync(
                "Sucesso",
                "Curso excluído com sucesso!",
                "OK"
            );
        }
    }
}