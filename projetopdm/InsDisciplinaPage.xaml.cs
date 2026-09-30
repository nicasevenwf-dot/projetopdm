using projetopdm.Data;
using projetopdm.Models;

namespace projetopdm;

public partial class InsDisciplinaPage : ContentPage
{
    private readonly Database _database;

    public InsDisciplinaPage()
    {
        InitializeComponent();

        _database = new Database();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        List<Curso> cursos = await _database.ListarCursos();

        pickerCurso.ItemsSource = cursos;
    }

    private async void InserirDisciplina(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtNome.Text))
        {
            await DisplayAlertAsync(
                "Atenção",
                "Digite o nome da disciplina.",
                "OK"
            );

            return;
        }

        if (string.IsNullOrWhiteSpace(txtSigla.Text))
        {
            await DisplayAlertAsync(
                "Atenção",
                "Digite a sigla da disciplina.",
                "OK"
            );

            return;
        }

        if (pickerCurso.SelectedItem == null)
        {
            await DisplayAlertAsync(
                "Atenção",
                "Selecione um curso.",
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

        Curso cursoSelecionado =
            (Curso)pickerCurso.SelectedItem;

        Disciplina disciplina = new Disciplina
        {
            DisNome = txtNome.Text,
            DisSigla = txtSigla.Text,
            DisObservacoes = txtObservacoes.Text,
            CurId = cursoSelecionado.CurId
        };

        await _database.InserirDisciplina(disciplina);

        await DisplayAlertAsync(
            "Sucesso",
            "Disciplina cadastrada com sucesso!",
            "OK"
        );

        await Shell.Current.GoToAsync("..");
    }
}