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

    private async void InserirCurso(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtNome.Text))
        {
            await DisplayAlertAsync(
                "Atenção",
                "Digite o nome do curso.",
                "OK"
            );

            return;
        }

        if (string.IsNullOrWhiteSpace(txtSigla.Text))
        {
            await DisplayAlertAsync(
                "Atenção",
                "Digite a sigla do curso.",
                "OK"
            );

            return;
        }

        if (pickerPeriodo.SelectedItem == null)
        {
            await DisplayAlertAsync(
                "Atenção",
                "Selecione um período.",
                "OK"
            );

            return;
        }

        if (string.IsNullOrWhiteSpace(txtObservacoes.Text))
        {
            await DisplayAlertAsync(
                "Atenção",
                "Digite as observações.",
                "OK"
            );

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

        await DisplayAlertAsync(
            "Sucesso",
            "Curso cadastrado com sucesso!",
            "OK"
        );

        await Shell.Current.GoToAsync("..");
    }
}