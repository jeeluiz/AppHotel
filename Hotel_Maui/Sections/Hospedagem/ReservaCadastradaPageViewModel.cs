using Hotel.Data.Context;
using Hotel.Data.Model;
using Hotel_Maui.Sections.Hospedes;
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

        private int quantidadeAdulto;
        private int quantidadeCriancas;

        public string Nome { get; set; }
        public double ValorTotal { get; set; }
        public int Dias { get; set; }
        public string Quarto { get; set; }
        public DateTime DataCheckIn { get; set; }
        public DateTime DataCheckOut { get; set; }
        public TimeSpan HoraCheckIn { get; set; }
        public TimeSpan HoraCheckOut { get; set; }
        public int QuantidadeAdulto
        {
            get { return quantidadeAdulto; }
            set
            {
                quantidadeAdulto = value;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(QuantidadeAdulto)));
            }
        }
        public int QuantidadeCrianca
        {
            get { return quantidadeCriancas; }
            set
            {
                quantidadeCriancas = value;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(QuantidadeCrianca)));
            }
        }

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


                IQueryable<Reserva> query = context.Hospedagems.Include(r => r.Hospede).Include(r => r.Quarto);

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

                    var vm = new HospedagemCalculadaPageViewModel
                    {
                        Nome = reserva.Hospede.Nome,
                        CPF = reserva.Hospede.CPF,
                        ValorTotal = reserva.ValorTotal,
                        Dias = reserva.QuantidadeDias,
                        Quarto = reserva.Quarto,
                        DataCheckIn = reserva.DataCheckIn,
                        DataCheckOut = reserva.DataCheckOut,
                        HoraCheckIn = reserva.HoraCheckIn,
                        HoraCheckOut = reserva.HoraCheckOut,
                        QuantidadeAdulto = reserva.QuantidadeAdultos,
                        QuantidadeCrianca = reserva.QuantidadeCrianca,

                    };

                    ((FlyoutPage)App.Current.MainPage).Detail =
                    new NavigationPage(new HospedagemCalculada(vm));
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
