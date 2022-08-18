using Hotel.Data.Context;
using Hotel.Data.Model;
using System.ComponentModel;
using System.Windows.Input;

namespace Hotel_Maui.Sections.Hospedagem
{
    [QueryProperty("PegarIdNavegacao", "parametro_id")]
    public class HospedagemCalculadaPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        int id_parametro;

        public string Nome { get; set; }
        public string CPF { get; set; }
        public double ValorTotal { get; set; }
        public int Dias { get; set; }
        public CategoriaQuarto Quarto { get; set; }
        public DateTime DataCheckIn { get; set; }
        public DateTime DataCheckOut { get; set; }
        public TimeSpan HoraCheckIn { get; set; }
        public TimeSpan HoraCheckOut { get; set; }
        public CadastroHospede Hospede { get; set; }
        public int QuantidadeAdulto { get; set; }
        public int QuantidadeCrianca { get; set; }

        public string PegarIdNavegacao
        {
            set
            {
                id_parametro = Convert.ToInt32(Uri.UnescapeDataString(value));

                VerAtividade.Execute(id_parametro);
            }
        }

        public ICommand VerAtividade
        {
            get => new Command<Guid>(async (id) =>
            {
                try
                {
                    using var context = new MeuDbContext(HotelMauiConstants.DbOptions);

                    Reserva model = await context.Hospedagems.FindAsync(id);

                    Nome = model.Hospede.Nome;
                    CPF = model.Hospede.CPF;
                    ValorTotal = model.ValorTotal;
                    Dias = model.QuantidadeDias;
                    Quarto = model.Quarto;
                    DataCheckIn = model.DataCheckIn;
                    DataCheckOut = model.DataCheckOut;
                    HoraCheckIn = model.HoraCheckIn;
                    HoraCheckOut = model.HoraCheckOut;
                    QuantidadeAdulto = model.QuantidadeAdultos;
                    QuantidadeCrianca = model.QuantidadeCrianca;
                }
                catch (Exception ex)
                {
                    await Application.Current.MainPage.DisplayAlert("Ops", ex.Message, "OK");

                }
            });
        }
    }
}
