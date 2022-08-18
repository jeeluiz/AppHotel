using Hotel.Data.Context;
using Hotel.Data.Enums;
using Hotel.Data.Model;
using Hotel_Maui.Helpers;
using Hotel_Maui.View;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace Hotel_Maui.Sections.Hospedagem
{
    public class ContratacaoHospedagemViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string _nome;

        public string ParametroBusca { get; set; }
        private CadastroHospede? Hospede { get; set; }
        private object quartoSelecionado;
        private int quantidadeAdulto;
        private int quantidadeCriancas;
        private DateTime dataChegada = DateTime.Now;
        private TimeSpan horaChegada = DateTime.Now.TimeOfDay;
        private DateTime dataSaida = DateTime.Now;
        private TimeSpan horaSaida = DateTime.Now.TimeOfDay;
        private double valorTotal { get; set; }

        public string Nome
        {
            get => string.IsNullOrEmpty(_nome) ? "Hospede" : _nome;
            set
            {
                _nome = value;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(Nome)));
            }
        }

        bool estaAtualizando = false;

        public bool EstaAtualizando
        {
            get => estaAtualizando;
            set
            {
                estaAtualizando = value;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(EstaAtualizando)));
            }
        }

        public ICommand BuscarHospedes
        {
            get
            {
                return new Command(async () =>
                await BuscarHospedesNoBanco());
            }
        }

        public ICommand AtualizarLista
        {
            get
            {
                return new Command(async () =>
                await BuscarHospedesNoBanco());
            }
        }

        public async Task BuscarHospedesNoBanco()
        {
            try
            {
                if (EstaAtualizando)
                {
                    return;
                }

                EstaAtualizando = true;
                if (ParametroBusca == null)
                {
                    await Application.Current.MainPage.DisplayAlert("ops", "Cpf invalido", "OK");
                    return;
                }

                using var context = new MeuDbContext(HotelMauiConstants.DbOptions);

                IQueryable<CadastroHospede> query = context.CadastroHospede;

                if (!string.IsNullOrEmpty(ParametroBusca))
                {
                    query = query.Where(h => h.CPF == ParametroBusca);

                }
                var result = await query.FirstOrDefaultAsync();


                if (result == null)
                {
                    await Application.Current.MainPage.DisplayAlert("ops", "Cpf nao encontrado", "OK");
                    return;
                }

                Nome = result.Nome;
                Hospede = result;
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

        public ObservableCollection<string> Quartos { get => new ObservableCollection<string>(Enum.GetValues<TipoQuarto>().Select(cq => cq.ToString()).ToList()); }

        public object QuartoSelecionado
        {
            get { return quartoSelecionado; }
            set { quartoSelecionado = value; }
        }

        public DateTime DataChegada
        {
            get { return dataChegada; }
            set { dataChegada = value; }
        }

        public TimeSpan HoraChegada
        {
            get { return horaChegada; }
            set { horaChegada = value; }
        }

        public DateTime DataSaida
        {
            get { return dataSaida; }
            set { dataSaida = value; }
        }


        public TimeSpan HoraSaida
        {
            get { return horaSaida; }
            set { horaSaida = value; }
        }


        public int QuantidadeCriancas
        {
            get { return quantidadeCriancas; }
            set
            {
                quantidadeCriancas = value;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(QuantidadeCriancas)));
            }
        }


        public int QuantidadeAdulto
        {
            get { return quantidadeAdulto; }
            set
            {
                quantidadeAdulto = value;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(QuantidadeAdulto)));
            }

        }

        public double ValorTotal
        {
            get { return valorTotal; }
            set
            {
                valorTotal = value;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(valorTotal)));
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
                    QuartoSelecionado = model.Quarto;
                    QuantidadeAdulto = model.QuantidadeAdultos;
                    QuantidadeCriancas = model.QuantidadeCrianca;
                    DataChegada = model.DataCheckIn;
                    HoraChegada = model.HoraCheckIn;
                    DataSaida = model.DataCheckOut;

                }
                catch (Exception ex)
                {
                    await Application.Current.MainPage.DisplayAlert("Ops", ex.Message, "OK");

                }
            });
        }

        public ICommand BotaoSalvar
        {
            get => new Command(async () =>
            {
                if (Hospede is null)
                {
                    await Application.Current.MainPage.DisplayAlert("Ops", "Procure Um hospede Primeiro", "OK");
                    return;
                }
                try
                {
                    using var context = new MeuDbContext(HotelMauiConstants.DbOptions);

                    var model = await context.Hospedagems.FirstOrDefaultAsync(h => h.Hospede == Hospede);

                    var enumQuarto = Enum.Parse<TipoQuarto>(QuartoSelecionado.ToString());
                    var quarto = enumQuarto.ParaCategoria();
                    var quartoDb = await context.CategoriaQuartos.FirstOrDefaultAsync(c => c.Id == quarto.Id);

                    var dias = (DataSaida - DataChegada).Days;

                    var valorTotal = quarto.ValorDiariaCrianca * QuantidadeCriancas * dias +
                                     quarto.ValorDiariaAdulto * QuantidadeAdulto * dias;

                    if (model == null)
                    {

                        if (quartoDb == null)
                        {
                            quartoDb = quarto;
                            await context.AddAsync(quartoDb);
                        }

                        if (DataSaida < DataChegada)
                        {
                            await Application.Current.MainPage.DisplayAlert("Ops", "A data de saida não pode ser menor que a data de chegada", "OK");
                            return;
                        }

                        context.Entry(Hospede).State = EntityState.Unchanged;

                        model = new Reserva
                        {
                            Quarto = quartoDb,
                            DataCheckIn = dataChegada,
                            DataCheckOut = dataSaida,
                            HoraCheckIn = horaChegada,
                            HoraCheckOut = horaSaida,
                            QuantidadeAdultos = quantidadeAdulto,
                            QuantidadeCrianca = quantidadeCriancas,
                            Hospede = Hospede,
                            ValorTotal = valorTotal,
                        };
                        await context.AddAsync(model);
                    }
                    else
                    {
                        model.Quarto = quartoDb;
                        model.DataCheckIn = dataChegada;
                        model.DataCheckOut = dataSaida;
                        model.HoraCheckIn = horaChegada;
                        model.HoraCheckOut = horaSaida;
                        model.ValorTotal = valorTotal;
                        model.QuantidadeAdultos = quantidadeAdulto;
                        model.QuantidadeCrianca = quantidadeCriancas;
                        context.Update(model);
                    }

                    await context.SaveChangesAsync();

                    var page = new HospedagemCalculada();

                    page.BindingContext = new HospedagemCalculadaPageViewModel()
                    {
                        Nome = Nome,
                        ValorTotal = valorTotal,
                        Dias = dias,
                        Quarto = quartoDb,
                        DataCheckIn = dataChegada,
                        DataCheckOut = dataSaida,
                        HoraCheckIn = horaChegada,
                        HoraCheckOut = horaSaida,
                        Hospede = Hospede,
                        QuantidadeAdulto = quantidadeAdulto,
                        QuantidadeCrianca = quantidadeCriancas,

                    };

                    ((FlyoutPage)App.Current.MainPage).Detail =
                    new NavigationPage(page);
                }
                catch (Exception ex)
                {
                    await Application.Current.MainPage.DisplayAlert("Ops", ex.Message, "OK");
                }
            });
        }

    }
}





