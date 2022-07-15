namespace Hotel_Maui.View;

public partial class Menu : FlyoutPage
{
	public Menu()
	{
		InitializeComponent();
	}

	async  void Button_Cadastro_Quarto(object sender, EventArgs e)
	{
		((FlyoutPage)App.Current.MainPage).Detail = new NavigationPage(new ContratacaoHospedagem());

    }
    async void Btn_CadastroCliente(object sender, EventArgs e)
    {
        ((FlyoutPage)App.Current.MainPage).Detail = new NavigationPage(new CadastroHospede());

    }
    
}