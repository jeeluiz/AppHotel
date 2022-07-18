using Hotel_Maui.Enums;
using Hotel_Maui.Helpers;
using System.Globalization;

namespace Hotel_Maui;

public partial class App : Application
{
    public List<Model.CategoriaQuarto> tipos_quartos = Enum.GetValues<TipoQuarto>().Select(e => e.ParaCategoria()).ToList();
   
    public App()
    {
        InitializeComponent();
        Thread.CurrentThread.CurrentCulture = new CultureInfo("pt-BR");
        MainPage = new View.Menu();
    }
}
