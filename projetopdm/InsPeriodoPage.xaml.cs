using projetopdm.Data;
using projetopdm.Models;

namespace projetopdm;

public partial class InsPeriodoPage : ContentPage
{
    private readonly Database _database;

    public InsPeriodoPage()
    {
        InitializeComponent();

        _database = new Database();
    }

    private async void InserirPeriodo(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtNome.Text))
        {
            await DisplayAlertAsync(
                "Atenção",
                "Digite o nome do período.",
                "OK"
            );

            return;
        }

        if (string.IsNullOrWhiteSpace(txtSigla.Text))
        {
            await DisplayAlertAsync(
                "Atenção",
                "Digite a sigla do período.",
                "OK"
            );

            return;
        }

        Periodo periodo = new Periodo
        {
            PerNome = txtNome.Text,
            PerSigla = txtSigla.Text
        };

        await _database.InserirPeriodo(periodo);

        await DisplayAlertAsync(
            "Sucesso",
            "Período cadastrado com sucesso!",
            "OK"
        );

        await Shell.Current.GoToAsync("..");
    }
}