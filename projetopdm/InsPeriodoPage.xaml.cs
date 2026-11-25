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

    private Task Aviso(string mensagem)
    {
        return popup.MostrarAsync(
            "Atenção",
            mensagem,
            "OK",
            null,
            "⚠️",
            Colors.DarkOrange
        );
    }

    private async void InserirPeriodo(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtNome.Text))
        {
            await Aviso("Digite o nome do período.");
            return;
        }

        if (string.IsNullOrWhiteSpace(txtSigla.Text))
        {
            await Aviso("Digite a sigla do período.");
            return;
        }

        if (string.IsNullOrWhiteSpace(txtData.Text))
        {
            await Aviso("Digite a data do período.");
            return;
        }

        Periodo periodo = new Periodo
        {
            PerNome = txtNome.Text,
            PerSigla = txtSigla.Text,
            PerData = txtData.Text
        };

        await _database.InserirPeriodo(periodo);

        await popup.MostrarAsync(
            "Sucesso",
            "Período cadastrado com sucesso!",
            "OK",
            null,
            "✅",
            Colors.Green
        );

        await Shell.Current.GoToAsync("..");
    }
}