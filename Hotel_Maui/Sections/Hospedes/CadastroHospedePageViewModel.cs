using Hotel.Data.Context;
using Hotel.Data.Model;
using System.ComponentModel;
using System.Windows.Input;

namespace Hotel_Maui.Sections.Hospedes
{
    [QueryProperty("PegarIdNavegacao", "parametro_id")]
    public class CadastroHospedePageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string nome, cpf, cep, numeroEndereco, endereco;
        private Guid id;

        public string PegarIdNavegacao
        {
            set
            {
                int id_parametro = Convert.ToInt32(Uri.UnescapeDataString(value));

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

        public ICommand NovaAtividade
        {
            get => new Command(() =>
            {
                Id = new Guid();
                Nome = string.Empty;
                Cep = string.Empty;
                CPF = string.Empty;
                Endereco = string.Empty;
                NumeroEndereco = string.Empty;
            });
        }

        public ICommand BotaoSalvar
        {
            get => new Command(async () =>
            {
                try
                {
                    using var context = new MeuDbContext(HotelMauiConstants.DbOptions);

                    var model = new CadastroHospede
                    {
                        Nome = Nome,
                        CPF = CPF,
                        Cep = Cep,
                        NumeroEndereco = NumeroEndereco,
                        Endereco = Endereco,
                    };

                    await context.AddAsync(model);
                    await context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    await Application.Current.MainPage.DisplayAlert("Ops", ex.Message, "OK");
                }
            });
        }
    }
}
