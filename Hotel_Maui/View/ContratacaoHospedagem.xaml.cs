using Hotel_Maui.Context;
using Hotel_Maui.Enums;
using Hotel_Maui.Model;
using System.ComponentModel;

namespace Hotel_Maui.View;

public partial class ContratacaoHospedagem : ContentPage
{
	App PropriedadesApp;
    public ContratacaoHospedagem()
	{
		InitializeComponent();

		PropriedadesApp = (App)Application.Current;
        pckquarto.ItemsSource = PropriedadesApp.tipos_quartos;

	//Definindo os valores Maximos e minimos  das datas
	//Cliente nao pode fazer Checkin no passado
	//Agendamento maximo 6 meses
		dtpck_data_checkin.MinimumDate = DateTime.Now;
		dtpck_data_checkin.MaximumDate = DateTime.Now.AddMonths(6);

		dtpck_data_checkout.MinimumDate = DateTime.Now.AddDays(1);
        dtpck_data_checkout.MaximumDate = DateTime.Now.AddMonths(6).AddDays(7);
    }

	private async void BtnCalcular_Clicked(object sender, EventArgs e)
	{
		try
		{
			int qnt_adultos = Convert.ToInt32(lbl_qnt_adulto.Text);
            int qnt_crianca = Convert.ToInt32(lbl_qnt_Crianca.Text);
						
            if (qnt_adultos == 0 && qnt_crianca == 0)
				throw new Exception("Desculpe, informe pelo menos um adulto.");
			
			
			if (qnt_adultos == 0)
                throw new Exception("Somente adultos podem alugar quartos");
			
			
			Model.CategoriaQuarto quarto_selecionado = (Model.CategoriaQuarto)pckquarto.SelectedItem;
            if (quarto_selecionado == null)
				throw new Exception("Desculpe, selecione um quarto");

			Model.Reserva dados_hospedagem = new Model.Reserva()
			{
				Quarto = quarto_selecionado,

				QuantidadeAdultos = qnt_adultos,
				QuantidadeCrianca = qnt_crianca,

				QuantidadeDias = Model.Reserva.CalcularTempoEstadia(dtpck_data_checkin.Date, dtpck_data_checkout.Date),

				DataCheckIn = dtpck_data_checkin.Date,
				DataCheckOut = dtpck_data_checkout.Date,
				HoraCheckIn = dtpck_hora_checkin.Time,
				HoraCheckOut = dtpck_hora_checkout.Time,
            };

			dados_hospedagem.ValorTotal = dados_hospedagem.CalcularValorEstadia();

			var segundaTela = new HospedagemCalculada();
			segundaTela.BindingContext = dados_hospedagem;

			await Navigation.PushAsync(segundaTela);
        }
		catch(Exception ex)
		{
			await DisplayAlert("Atenção", ex.Message, "OK");
		}
	}

	private void dtpck_data_checkin_DateSelected(object sender, DateChangedEventArgs e)
	{
		DatePicker elemento = (DatePicker)sender;
	
		DateTime data_checkin = elemento.Date;

        //dtpck_data_checkout.MinimumDate =  new DateTime(DateTime.Now.Year,DateTime.Now.Month,DateTime.Now.Day).AddDays(1);
		//12/07/2022 00:00
        dtpck_data_checkout.MinimumDate = DateTime.Now.AddDays(1);
        //12/07/2022 15:40

    }
     async void Button_Cadastro(object sender, EventArgs e)
    {
        await Navigation.PopAsync();

		var reserva = new Reserva()
		{
			QuantidadeAdultos = (int)StepperAdulto.Value,
			QuantidadeCrianca = (int)StepperCrianca.Value,
			Quarto = (CategoriaQuarto)pckquarto.SelectedItem,
			HoraCheckIn = dtpck_hora_checkin.Time,
			HoraCheckOut = dtpck_hora_checkout.Time,
			DataCheckIn = dtpck_data_checkin.Date,
			DataCheckOut = dtpck_data_checkout.Date,
        };

		reserva.ValorTotal = reserva.CalcularValorEstadia();

        using (var meuDbContext = new MeuDbContext())
        {
            meuDbContext.Add(reserva);

            await meuDbContext.SaveChangesAsync();
        }
    }

}