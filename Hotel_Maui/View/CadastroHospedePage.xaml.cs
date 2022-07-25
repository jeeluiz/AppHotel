
using Hotel.Data.Context;
using Hotel.Data.Model;

namespace Hotel_Maui.View;

public partial class CadastroHospedePage : ContentPage
{
	public CadastroHospedePage()
	{
		InitializeComponent();
	}

	private async void BtnSalvar_Clicked(object sender, EventArgs e)
	{
        //var hospede = new CadastroHospede { 
        //    Id = Guid.NewGuid(),
        //    Nome = EntNomeHospede.Text,
        //    CPF = EntryCPF.Text,
        //    Cep = EntryCep.Text,
        //    Endereco = EntryEndereco.Text,
        //    NumeroEndereco = EntryNumeroComp.Text,
        //};

       

        ////var hospedagem = new Reserva
        ////{
        ////    Id = Guid.NewGuid(),
        ////    Hospede = hospede,
        ////    Quarto = 
        ////};

        //using (var meuDbContext = new MeuDbContext(HotelMauiConstants.DbOptions))
        //{
        //    meuDbContext.Add(hospede);
        //    //meuDbContext.Add(hospedagem);
        //    try
        //    {
        //        await meuDbContext.SaveChangesAsync();
        //    }
        //    catch(Exception ex)
        //    {
        //        await DisplayAlert(ex.InnerException.Message, ex.InnerException.Message,"OK");
        //    }
        //}
    }
}