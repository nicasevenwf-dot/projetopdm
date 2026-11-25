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

    private async void InserirDisciplina(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtNome.Text))
        {
            await Aviso("Digite o nome da disciplina.");
            return;
        }

        if (string.IsNullOrWhiteSpace(txtSigla.Text))
        {
            await Aviso("Digite a sigla da disciplina.");
            return;
        }

        if (pickerCurso.SelectedItem == null)
        {
            await Aviso("Selecione um curso.");
            return;
        }

        if (string.IsNullOrWhiteSpace(txtObservacoes.Text))
        {
            await Aviso("Digite as observações.");
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

        await popup.MostrarAsync(
            "Sucesso",
            "Disciplina cadastrada com sucesso!",
            "OK",
            null,
            "✅",
            Colors.Green
        );

        await Shell.Current.GoToAsync("..");
    }
}