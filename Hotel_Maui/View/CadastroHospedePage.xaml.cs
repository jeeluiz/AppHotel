using Hotel_Maui.Context;
using Hotel_Maui.Enums;
using Hotel_Maui.Model;

namespace Hotel_Maui.View;

public partial class CadastroHospedePage : ContentPage
{
	public CadastroHospedePage()
	{
		InitializeComponent();
	}

	private async Task BtnSalvar_Clicked(object sender, EventArgs e)
	{
        var hospede = new CadastroHospede { 
            Id = Guid.NewGuid(),
            Nome = EntNomeHospede.Text,
            CPF = EntryCPF.Text,
            Cep = EntryCep.Text,
            Endereco = EntryEndereco.Text,
            NumeroEndereco = EntryNumeroComp.Text,
        };

       

        var hospedagem = new Reserva
        {
            Id = Guid.NewGuid(),
            Hospede = hospede,
        };

        using (var meuDbContext = new MeuDbContext())
        {
            meuDbContext.Add(hospede);
            meuDbContext.Add(hospedagem);

            await meuDbContext.SaveChangesAsync();
        }
    }
}