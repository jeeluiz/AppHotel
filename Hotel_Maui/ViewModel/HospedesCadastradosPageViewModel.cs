using Hotel.Data.Context;
using Hotel.Data.Model;
using Hotel_Maui.View;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace Hotel_Maui.ViewModel
{
    public class HospedesCadastradosPageViewModel : INotifyPropertyChanged 
    { 
        public CadastroHospede CadastroHospede { get; set; }
        public string ParametroBusca { get;  set; }

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


        public ObservableCollection<CadastroHospede> CadastrosHospedes { get; set; } = new ObservableCollection<CadastroHospede>();

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<CadastroHospede> ListaAtividades
        {
            get => CadastrosHospedes;
            set => CadastrosHospedes = value;
        }

        public ICommand BuscarHospedes
        {
            get
            {
                return new Command(async () =>
                {
                    try
                    {
                        if (EstaAtualizando)
                            return;

                        EstaAtualizando = true;
                       
                        using var context = new MeuDbContext(HotelMauiConstants.DbOptions);

                        ListaAtividades.Clear();

                        var result = context.CadastroHospede.Where(b => b.CPF == ParametroBusca).ToList();

                        result.ForEach(item => ListaAtividades.Add(item));
                    }
                    catch (Exception ex)
                    {
                        await Application.Current.MainPage.DisplayAlert("ops", ex.Message, "OK");
                    }
                    finally
                    {
                        EstaAtualizando = false;
                    }
                });

            }

        }

        public ICommand AtualizarLista
        {
            get
            {
                return new Command(async () =>
                {
                    try
                    {
                        if (EstaAtualizando)
                            return;
                        EstaAtualizando = true;
                        using var context = new MeuDbContext(HotelMauiConstants.DbOptions);

                        List<CadastroHospede> temp = context.CadastroHospede.ToList();
                        ListaAtividades.Clear();

                        temp.ForEach(i => CadastrosHospedes.Add(i));
                    }
                    catch (Exception ex)
                    {
                        await Application.Current.MainPage.DisplayAlert("ops", ex.Message, "OK");
                    }
                    finally
                    {
                        EstaAtualizando = false;
                    }
                });

            }

        }
            
        public ICommand AbrirDetalhesAtividade
        {
            get
            {
                return new Command<Guid>(async (Guid id) =>
                {
                    await Shell.Current.GoToAsync($"//CadastroHospede?parametro_id={id}");
                });
            }
        }

        public ICommand Remover
        {
            get
            {
                return new Command<CadastroHospede>(async (CadastroHospede) =>
                {
                    try
                    {
                        bool conf = await Application.Current.MainPage.DisplayAlert("Tem Certeza", "Excluir", "Sim", "Não");

                        if (conf)
                        {
                            using (var context = new MeuDbContext(HotelMauiConstants.DbOptions))
                            {
                                context.Remove(CadastroHospede.Id);

                                await context.SaveChangesAsync();
                            }

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
}

