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

    private async Task CarregarPeriodos()
    {
        List<Periodo> periodos = await _database.ListarPeriodos();

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

        bool confirmar = await DisplayAlertAsync(
            "Excluir período",
            $"Deseja realmente excluir {periodo.PerNome}?",
            "Excluir",
            "Cancelar"
        );

        if (confirmar)
        {
            await _database.ExcluirPeriodo(periodo);

            await CarregarPeriodos();

            await DisplayAlertAsync(
                "Sucesso",
                "Período excluído com sucesso!",
                "OK"
            );
        }
    }
}