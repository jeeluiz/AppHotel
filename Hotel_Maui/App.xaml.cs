using System.Globalization;

namespace Hotel_Maui;

public partial class App : Application
{
    public List<Model.CategoriaQuarto> tipos_quartos = new List<Model.CategoriaQuarto>()

    {
        new Model.CategoriaQuarto()
        {
            Descricao = "Suite Super Luxo",
            ValorDiariaAdulto = 110.0,
            ValorDiariaCrianca =55.0
        },

        new Model.CategoriaQuarto()
        {
            Descricao = "Suite Luxo",
            ValorDiariaAdulto = 80.0,
            ValorDiariaCrianca =40.0
        },
        new Model.CategoriaQuarto()
        {
            Descricao = "Suite Simples",
            ValorDiariaAdulto = 50.0,
            ValorDiariaCrianca =12.5
        },


    };
   
    public App()
    {
        InitializeComponent();
        Thread.CurrentThread.CurrentCulture = new CultureInfo("pt-BR");
        MainPage = new View.Menu();
    }
}
