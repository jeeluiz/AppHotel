
using Hotel.Data.Context;
using Hotel.Data.Model;
using Hotel_Maui.Sections.Hospedes;

namespace Hotel_Maui.View;

public partial class CadastroHospedePage : ContentPage
{
    public CadastroHospedePage(CadastroHospedePageViewModel? viewModel = null)
    {
        BindingContext = viewModel ?? new CadastroHospedePageViewModel();
        InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        var vm = (CadastroHospedePageViewModel)BindingContext;

        if (vm.Id == Guid.Empty)
        {
            vm.NovaAtividade.Execute(null);
        }

    }

  
}