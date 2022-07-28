using Hotel.Data.Context;
using Hotel.Data.Model;
using Hotel_Maui.View;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace Hotel_Maui.Sections.Hospedes
{
    public class HospedesCadastradosPageViewModel : INotifyPropertyChanged
    {
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

        public event PropertyChangedEventHandler PropertyChanged;


        ObservableCollection<CadastroHospede> cadastrosHospedes = new ObservableCollection<CadastroHospede>();

        public ObservableCollection<CadastroHospede> CadastrosHospedes
        {
            get => cadastrosHospedes;
            set => cadastrosHospedes = value;
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

                IQueryable<CadastroHospede> query = context.CadastroHospede;

                if (!string.IsNullOrEmpty(cpf))
                {
                    query = query.Where(h => h.CPF == cpf);
                }

                var result = await query.ToListAsync();

                CadastrosHospedes.Clear();

                result.ForEach(i => CadastrosHospedes.Add(i));
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
                    var hospede = CadastrosHospedes.FirstOrDefault(h => h.Id == id);

                    var vm = new CadastroHospedePageViewModel
                    {

                        Cep = hospede.Cep,
                        CPF = hospede.CPF,
                        Nome = hospede.Nome,
                        Endereco = hospede.Endereco,
                        NumeroEndereco = hospede.NumeroEndereco,
                    };

                    ((FlyoutPage) App.Current.MainPage).Detail = 
                    new NavigationPage(new CadastroHospedePage(vm));
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

                        var hospede = context.CadastroHospede.FirstOrDefault(h => h.Id == id);

                        if (hospede is null)
                        {
                            await Application.Current.MainPage.DisplayAlert("404: Not Found!", "Usuário não encontrado!", "OK");

                            return;
                        }

                        context.Remove(hospede);

                        await context.SaveChangesAsync();

                        var itemRemover = CadastrosHospedes.First(e => e.Id == id);
                        CadastrosHospedes.Remove(itemRemover);
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

