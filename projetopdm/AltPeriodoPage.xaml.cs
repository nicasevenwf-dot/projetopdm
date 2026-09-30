using projetopdm.Data;
using projetopdm.Models;

namespace projetopdm;

[QueryProperty(nameof(PeriodoSelecionado), "Periodo")]
public partial class AltPeriodoPage : ContentPage
{
    private readonly Database _database;

    private Periodo _periodoSelecionado;

    public Periodo PeriodoSelecionado
    {
        set
        {
            _periodoSelecionado = value;

            txtNome.Text = value.PerNome;
            txtSigla.Text = value.PerSigla;
        }
    }

    public AltPeriodoPage()
    {
        InitializeComponent();

        _database = new Database();
    }

    private async void AlterarPeriodo(object sender, EventArgs e)
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

        _periodoSelecionado.PerNome = txtNome.Text;
        _periodoSelecionado.PerSigla = txtSigla.Text;

        await _database.AlterarPeriodo(_periodoSelecionado);

        await DisplayAlertAsync(
            "Sucesso",
            "Período alterado com sucesso!",
            "OK"
        );

        await Shell.Current.GoToAsync("..");
    }
}