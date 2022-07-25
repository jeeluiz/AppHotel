using Hotel.Data.Context;
using Hotel.Data.Model;
using Hotel_Maui.ViewModel;

namespace Hotel_Maui.View;

public partial class HospedesCadastrados : ContentPage
{
    public HospedesCadastrados()
    {
        BindingContext = new CadastroHospedePage();
        InitializeComponent();
        listaReserva.ItemsSource = BuscarHospedes();
    }
     List<CadastroHospede> BuscarHospedes(string? cpf = null)
    {
        using var context = new MeuDbContext(HotelMauiConstants.DbOptions) ;
        
        IQueryable<CadastroHospede> query = context.CadastroHospede;

        if(!string.IsNullOrEmpty(cpf))
        {
            query = query.Where(h => h.CPF == cpf);
        }

        return query.ToList();
    }

    private void Buscar_Clicked(object sender, EventArgs e)
    {
        listaReserva.ItemsSource = BuscarHospedes(CPFHospede.Text);
    }

    private void listaReserva_ItemTapped(object sender, ItemTappedEventArgs e)
    {
        var page = new CadastroHospedePage()
        {
            BindingContext = new CadastroHospedePageViewModel()
            {
                CadastroHospede = (CadastroHospede)e.Item
            }
        };
        ((FlyoutPage)App.Current.MainPage).Detail = new NavigationPage(page);
    }
}