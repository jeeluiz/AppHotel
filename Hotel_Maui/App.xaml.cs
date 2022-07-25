using Hotel.Data.Enums;
using Hotel.Data.Model;
using Hotel_Maui.Helpers;
using System.Globalization;

namespace Hotel_Maui;

public partial class App : Application
{
    public List<CategoriaQuarto> tipos_quartos = Enum.GetValues<TipoQuarto>().Select(e => e.ParaCategoria()).ToList();
   
    public App()
    {
        InitializeComponent();
        Thread.CurrentThread.CurrentCulture = new CultureInfo("pt-BR");
        MainPage = new View.Menu();
    }
}
