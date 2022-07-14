namespace Hotel_Maui.View;

public partial class HospedagemCalculada : ContentPage
{
	public HospedagemCalculada()
	{
		InitializeComponent();
	}

	private async void Button_Clicked(object sender, EventArgs e)
	{
		await Navigation.PopAsync();
	}

	private void Button_Clicked_1(object sender, EventArgs e)
	{

	}
}