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
            if (value == null)
                return;

            _periodoSelecionado = value;

            txtNome.Text = value.PerNome;
            txtSigla.Text = value.PerSigla;
            txtData.Text = value.PerData;   // NOVO
        }
    }

    public AltPeriodoPage()
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

    private async void AlterarPeriodo(object sender, EventArgs e)
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

        _periodoSelecionado.PerNome = txtNome.Text;
        _periodoSelecionado.PerSigla = txtSigla.Text;
        _periodoSelecionado.PerData = txtData.Text;   // NOVO

        await _database.AlterarPeriodo(_periodoSelecionado);

        await popup.MostrarAsync(
            "Sucesso",
            "Período alterado com sucesso!",
            "OK",
            null,
            "✅",
            Colors.Green
        );

        await Shell.Current.GoToAsync("..");
    }
}