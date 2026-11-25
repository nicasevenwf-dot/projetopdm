using projetopdm.Data;
using projetopdm.Models;

namespace projetopdm;

public partial class InsCursoPage : ContentPage
{
    private readonly Database _database;

    public InsCursoPage()
    {
        InitializeComponent();

        _database = new Database();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        List<Periodo> periodos = await _database.ListarPeriodos();

        pickerPeriodo.ItemsSource = periodos;
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

    private async void InserirCurso(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtNome.Text))
        {
            await Aviso("Digite o nome do curso.");
            return;
        }

        if (string.IsNullOrWhiteSpace(txtSigla.Text))
        {
            await Aviso("Digite a sigla do curso.");
            return;
        }

        if (pickerPeriodo.SelectedItem == null)
        {
            await Aviso("Selecione um período.");
            return;
        }

        if (string.IsNullOrWhiteSpace(txtObservacoes.Text))
        {
            await Aviso("Digite as observações.");
            return;
        }

        Periodo periodoSelecionado =
            (Periodo)pickerPeriodo.SelectedItem;

        Curso curso = new Curso
        {
            CurNome = txtNome.Text,
            CurSigla = txtSigla.Text,
            CurObservacoes = txtObservacoes.Text,
            PerId = periodoSelecionado.PerId
        };

        await _database.InserirCurso(curso);

        await popup.MostrarAsync(
            "Sucesso",
            "Curso cadastrado com sucesso!",
            "OK",
            null,
            "✅",
            Colors.Green
        );

        await Shell.Current.GoToAsync("..");
    }
}