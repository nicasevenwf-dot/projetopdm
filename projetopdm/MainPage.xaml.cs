using projetopdm.Data;
using projetopdm.Models;

namespace projetopdm
{
    public partial class MainPage : ContentPage
    {
        private readonly Database _database;

        public MainPage()
        {
            InitializeComponent();

            _database = new Database();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            List<Curso> cursos = await _database.ListarCursos();

            listaCursos.ItemsSource = cursos;
        }
    }
}