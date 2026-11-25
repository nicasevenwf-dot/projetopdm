namespace projetopdm;

public partial class PopupConfirmacao : ContentView
{
    private TaskCompletionSource<bool>? _resultado;

    public PopupConfirmacao()
    {
        InitializeComponent();
    }

    public Task<bool> MostrarAsync(
        string titulo,
        string mensagem,
        string textoConfirmar = "OK",
        string? textoCancelar = null,
        string icone = "⚠️",
        Color? corConfirmar = null)
    {
        lblIcone.Text = icone;
        lblTitulo.Text = titulo;
        lblMensagem.Text = mensagem;

        btnConfirmar.Text = textoConfirmar;
        btnConfirmar.BackgroundColor = corConfirmar ?? Colors.Red;

        bool temCancelar = textoCancelar != null;
        btnCancelar.IsVisible = temCancelar;
        btnCancelar.Text = textoCancelar ?? string.Empty;

        // Sem botão cancelar, o confirmar ocupa a linha toda
        Grid.SetColumn(btnConfirmar, temCancelar ? 1 : 0);
        Grid.SetColumnSpan(btnConfirmar, temCancelar ? 1 : 2);

        _resultado = new TaskCompletionSource<bool>();
        IsVisible = true;

        return _resultado.Task;
    }

    private void OnConfirmarClicked(object sender, EventArgs e) => Fechar(true);

    private void OnCancelarClicked(object sender, EventArgs e) => Fechar(false);

    private void Fechar(bool resultado)
    {
        IsVisible = false;
        _resultado?.TrySetResult(resultado);
    }
}