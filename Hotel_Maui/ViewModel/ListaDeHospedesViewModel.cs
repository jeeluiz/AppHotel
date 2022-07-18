using Hotel_Maui.Context;
using Hotel_Maui.Model;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace Hotel_Maui.ViewModel
{
    public class ListaDeHospedesViewModel : INotifyPropertyChanged
    {
        public string CPF { get; set; }
        public string Nome { get; set; }

        public string ParametroBuscar { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        ObservableCollection<Reserva> listaReserva = new ObservableCollection<Reserva>();


        public ICommand BuscaCpfLista
        {
            get
            {
                return new Command(async () =>
                {
                    try
                    {
                        using (var context = new MeuDbContext())
                        {
                            var cadastroHospede = context.CadastroHospede
                                .Single(b => b.CPF == "");
                        }
                    }
                    catch (Exception ex)
                    {
                        await Application.Current.MainPage.DisplayAlert("ops", ex.Message, "OK");
                    }
                    finally
                    {

                    }
                });

            }

        }


        public ICommand AbrirDetalhesAtividade
        {
            get
            {
                return new Command<int>(async (int id) =>
                {
                    await Shell.Current.GoToAsync($"//CadastroHospedagem?parametro_id={id}");
                });
            }
        }
        
    }
}
