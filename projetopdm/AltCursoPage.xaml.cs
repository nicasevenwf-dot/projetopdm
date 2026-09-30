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

    private async void AlterarCurso(object sender, EventArgs e)
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

        _cursoSelecionado.CurNome = txtNome.Text;
        _cursoSelecionado.CurSigla = txtSigla.Text;
        _cursoSelecionado.CurObservacoes = txtObservacoes.Text;
        _cursoSelecionado.PerId = periodoSelecionado.PerId;

        await _database.AlterarCurso(_cursoSelecionado);

        await DisplayAlertAsync(
            "Sucesso",
            "Curso alterado com sucesso!",
            "OK"
        );

        await Shell.Current.GoToAsync("..");
    }
}