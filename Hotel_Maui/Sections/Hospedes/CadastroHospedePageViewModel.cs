using Hotel.Data.Context;
using Hotel.Data.Model;
using Hotel_Maui.View;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.Windows.Input;

namespace Hotel_Maui.Sections.Hospedes
{
    [QueryProperty("PegarIdNavegacao", "parametro_id")]
    public class CadastroHospedePageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        int id_parametro;
        private string nome, cpf, cep, numeroEndereco, endereco;
        private Guid id;

      

        public string PegarIdNavegacao
        {
            set
            {
                id_parametro = Convert.ToInt32(Uri.UnescapeDataString(value));

                VerAtividade.Execute(id_parametro);
            }
        }


        public string Nome
        {
            get => nome;
            set
            {
                nome = value;
                //PropertyChanged(this, new PropertyChangedEventArgs(nameof(Nome)));
            }
        }

        public string CPF
        {
            get => cpf;
            set
            {
                cpf = value;
                //PropertyChanged(this, new PropertyChangedEventArgs(nameof(CPF)));
            }
        }

        public Guid Id
        {
            get => id;
            set
            {
                id = value;
                //PropertyChanged(this, new PropertyChangedEventArgs(nameof(Id)));
            }
        }

        public string Cep
        {
            get => cep;
            set
            {
                cep = value;
                //PropertyChanged(this, new PropertyChangedEventArgs(nameof(Cep)));
            }
        }

        public string NumeroEndereco
        {
            get => numeroEndereco;
            set
            {
                numeroEndereco = value;
                //PropertyChanged(this, new PropertyChangedEventArgs(nameof(NumeroEndereco)));
            }
        }

        public string Endereco
        {
            get => endereco;
            set
            {
                endereco = value;
                //PropertyChanged(this, new PropertyChangedEventArgs(nameof(Endereco)));
            }
        }

        public ICommand VerAtividade
        {
            get => new Command<Guid>(async (id) =>
            {
                try
                {
                    using var context = new MeuDbContext(HotelMauiConstants.DbOptions);
                    CadastroHospede model = await context.CadastroHospede.FindAsync(id);

                    Id = model.Id;
                    Nome = model.Nome;
                    CPF = model.CPF;
                    Cep = model.Cep;
                    NumeroEndereco = model.NumeroEndereco;
                    Endereco = model.Endereco;
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
                try
                {
                    using var context = new MeuDbContext(HotelMauiConstants.DbOptions);

                    var model = await context.CadastroHospede.FirstOrDefaultAsync(h => h.CPF == CPF);

                    if(model == null)
                    {
                        model = new CadastroHospede
                        {
                            CPF = CPF,
                            Nome = Nome,
                            Cep = Cep,
                            Endereco = Endereco,
                            NumeroEndereco = NumeroEndereco,
                        };
                        await context.AddAsync(model);
                    }else
                    {
                        model.Nome = Nome;
                        model.Cep = Cep;
                        model.Endereco = Endereco;
                        model.NumeroEndereco = NumeroEndereco;

                        context.Update(model);
                    }

                    await context.SaveChangesAsync();
                    ((FlyoutPage)App.Current.MainPage).Detail =
                    new NavigationPage(new HospedesCadastrados());
                }
                catch (Exception ex)
                {
                    await Application.Current.MainPage.DisplayAlert("Ops", ex.Message, "OK");
                }
            });
        }
    }
}
