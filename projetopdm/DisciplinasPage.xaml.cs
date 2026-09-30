using projetopdm.Data;
using projetopdm.Models;

namespace projetopdm;

public partial class DisciplinasPage : ContentPage
{
    private readonly Database _database;

    public DisciplinasPage()
    {
        InitializeComponent();

        _database = new Database();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        await CarregarDisciplinas();
    }

    private async Task CarregarDisciplinas()
    {
        List<Disciplina> disciplinas =
            await _database.ListarDisciplinas();

        listaDisciplinas.ItemsSource = disciplinas;
    }

    private async void AbrirInserirDisciplina(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("InsDisciplinaPage");
    }

    private async void AbrirAlterarDisciplina(object sender, EventArgs e)
    {
        Button botao = (Button)sender;

        Disciplina disciplina =
            (Disciplina)botao.CommandParameter;

        await Shell.Current.GoToAsync(
            "AltDisciplinaPage",
            new Dictionary<string, object>
            {
            { "Disciplina", disciplina }
            }
        );
    }

    private async void ExcluirDisciplina(object sender, EventArgs e)
    {
        Button botao = (Button)sender;

        Disciplina disciplina =
            (Disciplina)botao.CommandParameter;

        bool confirmar = await DisplayAlertAsync(
            "Excluir disciplina",
            $"Deseja realmente excluir {disciplina.DisNome}?",
            "Excluir",
            "Cancelar"
        );

        if (confirmar)
        {
            await _database.ExcluirDisciplina(disciplina);

            await CarregarDisciplinas();

            await DisplayAlertAsync(
                "Sucesso",
                "Disciplina excluída com sucesso!",
                "OK"
            );
        }
    }
}