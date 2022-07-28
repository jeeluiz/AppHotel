
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
}