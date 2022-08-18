using Hotel_Maui.Sections.Hospedagem;

namespace Hotel_Maui.View;

public partial class Menu : FlyoutPage
{
    public Menu()
    {
        InitializeComponent();
    }

    private void Button_Cadastro_Quarto(object sender, EventArgs e)
    {
        ((FlyoutPage)App.Current.MainPage).Detail = new NavigationPage(new ContratacaoHospedagem());

    }
    private void Btn_CadastroCliente(object sender, EventArgs e)
    {
        ((FlyoutPage)App.Current.MainPage).Detail = new NavigationPage(new CadastroHospedePage());

    }

    private void Btn_Home(object sender, EventArgs e)
    {
        ((FlyoutPage)App.Current.MainPage).Detail = new NavigationPage(new HospedesCadastrados());
    }

    private void Button_Reservas_Cadastradas(object sender, EventArgs e)
    {
        ((FlyoutPage)App.Current.MainPage).Detail = new NavigationPage(new ReservaCadastrada());
    }


}