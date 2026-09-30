namespace projetopdm
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(
            "InsPeriodoPage",
            typeof(InsPeriodoPage)
        );
            Routing.RegisterRoute(
            "AltPeriodoPage",
            typeof(AltPeriodoPage)
);
            Routing.RegisterRoute(
            "InsCursoPage",
            typeof(InsCursoPage)
);
            Routing.RegisterRoute(
            "AltCursoPage",
            typeof(AltCursoPage)
);

            Routing.RegisterRoute(
            "InsDisciplinaPage",
            typeof(InsDisciplinaPage)
);
            Routing.RegisterRoute(
            "AltDisciplinaPage",
            typeof(AltDisciplinaPage)
            );
        }
    }
}
