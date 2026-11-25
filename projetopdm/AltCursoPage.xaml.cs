using projetopdm.Data;
using projetopdm.Models;

namespace projetopdm;

[QueryProperty(nameof(CursoSelecionado), "Curso")]
public partial class AltCursoPage : ContentPage
{
    private readonly Database _database;
    private Curso _cursoSelecionado;

    public Curso CursoSelecionado
    {
        set
        {
            if (value == null)
                return;

            _cursoSelecionado = value;

            txtNome.Text = value.CurNome;
            txtSigla.Text = value.CurSigla;
            txtObservacoes.Text = value.CurObservacoes;
        }
    }

    public AltCursoPage()
    {
        InitializeComponent();

        _database = new Database();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        List<Periodo> periodos = await _database.ListarPeriodos();

        pickerPeriodo.ItemsSource = periodos;

        if (_cursoSelecionado != null)
        {
            Periodo? periodoDoCurso = periodos.FirstOrDefault(
                p => p.PerId == _cursoSelecionado.PerId
            );

            pickerPeriodo.SelectedItem = periodoDoCurso;
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

    private async void AlterarCurso(object sender, EventArgs e)
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

        _cursoSelecionado.CurNome = txtNome.Text;
        _cursoSelecionado.CurSigla = txtSigla.Text;
        _cursoSelecionado.CurObservacoes = txtObservacoes.Text;
        _cursoSelecionado.PerId = periodoSelecionado.PerId;

        await _database.AlterarCurso(_cursoSelecionado);

        await popup.MostrarAsync(
            "Sucesso",
            "Curso alterado com sucesso!",
            "OK",
            null,
            "✅",
            Colors.Green
        );

        await Shell.Current.GoToAsync("..");
    }
}