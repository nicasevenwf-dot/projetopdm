using projetopdm.Data;
using projetopdm.Models;

namespace projetopdm;

[QueryProperty(nameof(DisciplinaSelecionada), "Disciplina")]
public partial class AltDisciplinaPage : ContentPage
{
    private readonly Database _database;
    private Disciplina _disciplinaSelecionada;

    public Disciplina DisciplinaSelecionada
    {
        set
        {
            if (value == null)
                return;

            _disciplinaSelecionada = value;

            txtNome.Text = value.DisNome;
            txtSigla.Text = value.DisSigla;
            txtObservacoes.Text = value.DisObservacoes;
        }
    }

    public AltDisciplinaPage()
    {
        InitializeComponent();

        _database = new Database();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        List<Curso> cursos = await _database.ListarCursos();

        pickerCurso.ItemsSource = cursos;

        if (_disciplinaSelecionada != null)
        {
            Curso? cursoDaDisciplina = cursos.FirstOrDefault(
                c => c.CurId == _disciplinaSelecionada.CurId
            );

            pickerCurso.SelectedItem = cursoDaDisciplina;
        }
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

    private async void AlterarDisciplina(object sender, EventArgs e)
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

        _disciplinaSelecionada.DisNome = txtNome.Text;
        _disciplinaSelecionada.DisSigla = txtSigla.Text;
        _disciplinaSelecionada.DisObservacoes = txtObservacoes.Text;
        _disciplinaSelecionada.CurId = cursoSelecionado.CurId;

        await _database.AlterarDisciplina(
            _disciplinaSelecionada
        );

        await popup.MostrarAsync(
            "Sucesso",
            "Disciplina alterada com sucesso!",
            "OK",
            null,
            "✅",
            Colors.Green
        );

        await Shell.Current.GoToAsync("..");
    }
}