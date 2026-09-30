using projetopdm.Data;

namespace projetopdm;

public partial class App : Application
{
    private readonly Database _database;

    public App()
    {
        InitializeComponent();

        _database = new Database();

        InicializarBanco();
    }

    private async void InicializarBanco()
    {
        await _database.Inicializar();
    }

    protected override Window CreateWindow(IActivationState? activationState)
    {
        return new Window(new AppShell());
    }
}