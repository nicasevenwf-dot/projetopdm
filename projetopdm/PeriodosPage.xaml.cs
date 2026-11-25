using projetopdm.Data;
using projetopdm.Models;

namespace projetopdm;

public partial class PeriodosPage : ContentPage
{
    private readonly Database _database;

    public PeriodosPage()
    {
        InitializeComponent();

        _database = new Database();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        await CarregarPeriodos();
    }

    private async void OnPesquisarTextChanged(object sender, EventArgs e)
    {
        await CarregarPeriodos();
    }

    private async Task CarregarPeriodos()
    {
        string texto = searchBar.Text ?? string.Empty;

        List<Periodo> periodos = string.IsNullOrWhiteSpace(texto)
            ? await _database.ListarPeriodos()
            : await _database.PesquisarPeriodos(texto);

        listaPeriodos.ItemsSource = periodos;
    }

    private async void AbrirInserirPeriodo(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("InsPeriodoPage");
    }

    private async void AbrirAlterarPeriodo(object sender, EventArgs e)
    {
        Button botao = (Button)sender;

        Periodo periodo = (Periodo)botao.CommandParameter;

        await Shell.Current.GoToAsync(
            "AltPeriodoPage",
            new Dictionary<string, object>
            {
                { "Periodo", periodo }
            }
        );
    }

    private async void ExcluirPeriodo(object sender, EventArgs e)
    {
        Button botao = (Button)sender;

        Periodo periodo = (Periodo)botao.CommandParameter;

        bool confirmar = await popup.MostrarAsync(
            "Excluir período",
            $"Deseja realmente excluir {periodo.PerNome}?",
            "Excluir",
            "Cancelar"
        );

        if (!confirmar)
            return;

        int resultado = await _database.ExcluirPeriodo(periodo);

        if (resultado == 0)
        {
            await popup.MostrarAsync(
                "Atenção",
                "Não é possível excluir: existem cursos vinculados a este período.",
                "OK",
                null,
                "⚠️",
                Colors.DarkOrange
            );

            return;
        }

        await CarregarPeriodos();

        await popup.MostrarAsync(
            "Sucesso",
            "Período excluído com sucesso!",
            "OK",
            null,
            "✅",
            Colors.Green
        );
    }
}