using Hotel_Maui.Sections.Hospedagem;

namespace Hotel_Maui.View;

public partial class HospedagemCalculada : ContentPage
{
    public HospedagemCalculada(HospedagemCalculadaPageViewModel? viewModel = null)
    {
        BindingContext = viewModel ?? new HospedagemCalculadaPageViewModel();
        InitializeComponent();
    }
}