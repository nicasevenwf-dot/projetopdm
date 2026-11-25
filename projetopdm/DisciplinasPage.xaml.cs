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

    private async void OnPesquisarTextChanged(object sender, EventArgs e)
    {
        await CarregarDisciplinas();
    }

    private async Task CarregarDisciplinas()
    {
        string texto = searchBar.Text ?? string.Empty;

        List<Disciplina> disciplinas = string.IsNullOrWhiteSpace(texto)
            ? await _database.ListarDisciplinas()
            : await _database.PesquisarDisciplinas(texto);

        listaDisciplinas.ItemsSource = disciplinas;
    }

    private async void AbrirInserirDisciplina(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("InsDisciplinaPage");
    }

    private async void AbrirAlterarDisciplina(object sender, EventArgs e)
    {
        Button botao = (Button)sender;

        Disciplina disciplina = (Disciplina)botao.CommandParameter;

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

        Disciplina disciplina = (Disciplina)botao.CommandParameter;

        bool confirmar = await popup.MostrarAsync(
            "Excluir disciplina",
            $"Deseja realmente excluir {disciplina.DisNome}?",
            "Excluir",
            "Cancelar"
        );

        if (!confirmar)
            return;

        await _database.ExcluirDisciplina(disciplina);

        await CarregarDisciplinas();

        await popup.MostrarAsync(
            "Sucesso",
            "Disciplina excluída com sucesso!",
            "OK",
            null,
            "✅",
            Colors.Green
        );
    }
}