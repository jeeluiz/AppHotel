using Hotel.Data.Context;
using Hotel.Data.Model;
using Hotel_Maui.View;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace Hotel_Maui.Sections.Hospedagem
{
    public class ReservaCadastradaPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public string ParametroBusca { get; set; }

        bool estaAtualizando = false;

        public bool EstaAtualizando
        {
            get => estaAtualizando;
            set
            {
                estaAtualizando = value;
                PropertyChanged(this, new PropertyChangedEventArgs("EstaAtualizando"));
            }
        }

        ObservableCollection<Reserva> reservasCadastradas = new ObservableCollection<Reserva>();

        public ObservableCollection<Reserva> ReservasCadastradas
        {
            get => reservasCadastradas;
            set => reservasCadastradas = value;
        }

        public ICommand BuscarHospedes { get { return new Command(async () => await BuscarHospedesNoBanco(ParametroBusca)); } }

        public ICommand AtualizarLista { get { return new Command(async () => await BuscarHospedesNoBanco()); } }

        public async Task BuscarHospedesNoBanco(string cpf = null)
        {
            try
            {
                if (EstaAtualizando)
                {
                    return;
                }

                EstaAtualizando = true;

                using var context = new MeuDbContext(HotelMauiConstants.DbOptions);


                IQueryable<Reserva> query = context.Hospedagems.Include(r => r.Hospede);

                if (!string.IsNullOrEmpty(cpf))
                {
                    query = query.Where(h => h.Hospede.CPF == cpf);
                }

                var result = await query.ToListAsync();

                ReservasCadastradas.Clear();

                result.ForEach(i => ReservasCadastradas.Add(i));
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("ops", ex.Message, "OK");
            }
            finally
            {
                EstaAtualizando = false;
            }
        }

        public ICommand AbrirDetalhesAtividade
        {
            get
            {
                return new Command<Guid>((id) =>
                {
                    var reserva = ReservasCadastradas.FirstOrDefault(h => h.Id == id);

                    var vm = new ContratacaoHospedagemViewModel
                    {

                        Nome = reserva.Hospede.Nome,
                        QuartoSelecionado = reserva.Quarto,
                        QuantidadeAdulto = reserva.QuantidadeAdultos,
                        QuantidadeCriancas = reserva.QuantidadeCrianca,
                        DataChegada = reserva.DataCheckIn,
                        HoraChegada = reserva.HoraCheckIn,
                        DataSaida = reserva.DataCheckOut,
                        HoraSaida = reserva.HoraCheckOut
                    };

                    ((FlyoutPage)App.Current.MainPage).Detail =
                    new NavigationPage(new ContratacaoHospedagem());
                });
            }
        }

        public ICommand Remover
        {
            get => new Command<Guid>(async (id) =>
            {
                try
                {
                    bool conf = await Application.Current.MainPage.DisplayAlert("Tem Certeza", "Excluir", "Sim", "Não");

                    if (conf)
                    {
                        using var context = new MeuDbContext(HotelMauiConstants.DbOptions);

                        var hospede = context.Hospedagems.FirstOrDefault(h => h.Id == id);

                        if (hospede is null)
                        {
                            await Application.Current.MainPage.DisplayAlert("404: Not Found!", "Usuário não encontrado!", "OK");

                            return;
                        }

                        context.Remove(hospede);

                        await context.SaveChangesAsync();

                        var itemRemover = ReservasCadastradas.First(e => e.Id == id);
                        ReservasCadastradas.Remove(itemRemover);
                    }
                }
                catch (Exception ex)
                {
                    await Application.Current.MainPage.DisplayAlert("ops", ex.Message, "OK");
                }
            });
        }

    }
}
