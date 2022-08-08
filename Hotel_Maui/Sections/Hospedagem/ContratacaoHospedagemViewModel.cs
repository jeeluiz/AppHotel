﻿using Hotel.Data.Context;
using Hotel.Data.Enums;
using Hotel.Data.Model;
using Hotel_Maui.Helpers;
using Hotel_Maui.View;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
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
            set { quantidadeCriancas = value;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(QuantidadeCriancas)));}
            }


        public int QuantidadeAdulto
        {
            get { return quantidadeAdulto; }
            set { quantidadeAdulto = value; 
            PropertyChanged(this, new PropertyChangedEventArgs(nameof(QuantidadeAdulto)));}
        
        }




        public ICommand BotaoSalvar
        {
            get => new Command(async () =>
            {
                if(Hospede is null)
                {
                    await Application.Current.MainPage.DisplayAlert("Ops", "Procure Um hospede Primeiro", "OK");
                    return;
                }
                try
                {
                    using var context = new MeuDbContext(HotelMauiConstants.DbOptions);

                    var model = await context.Hospedagems.FirstOrDefaultAsync(h => h.Hospede== Hospede);

                    if (model == null)
                    {
                        var enumQuarto = Enum.Parse<TipoQuarto>(QuartoSelecionado.ToString());
                        var quarto = enumQuarto.ParaCategoria();

                        var quartoDb = await context.CategoriaQuartos.FirstOrDefaultAsync(c => c.Id == quarto.Id);
                        if(quartoDb == null)
                        {
                            quartoDb = quarto;
                            await context.AddAsync(quartoDb);
                        }

                        if (DataSaida < DataChegada)
                        {
                            await Application.Current.MainPage.DisplayAlert("Ops","A data de saida não pode ser menor que a data de chegada", "OK");
                            return;
                        }
                        var dias = (DataSaida - DataChegada).Days;

                        context.Entry(Hospede).State = EntityState.Unchanged;

                        model = new Reserva
                        {
                            Quarto = quartoDb,
                            DataCheckIn = dataChegada,
                            DataCheckOut = dataSaida,
                            HoraCheckIn = horaChegada,
                            HoraCheckOut = horaSaida,
                            Hospede = Hospede,
                            ValorTotal = quarto.ValorDiariaCrianca * QuantidadeCriancas * dias + 
                                         quarto.ValorDiariaAdulto * QuantidadeAdulto * dias,
                        };
                        await context.AddAsync(model);
                    }
                    else
                    {
                        model.DataCheckIn = dataChegada;
                        model.DataCheckOut = dataSaida;
                        model.HoraCheckIn = horaChegada;
                        model.HoraCheckOut = horaSaida;

                        context.Update(model);
                    }

                    await context.SaveChangesAsync();
                    ((FlyoutPage)App.Current.MainPage).Detail =
                    new NavigationPage(new HospedagemCalculada());
                }
                catch (Exception ex)
                {
                    await Application.Current.MainPage.DisplayAlert("Ops", ex.Message, "OK");
                }
            });
        }

    }
}




