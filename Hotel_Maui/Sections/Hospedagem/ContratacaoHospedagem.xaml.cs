using Hotel_Maui.Sections.Hospedagem;

namespace Hotel_Maui.View;

public partial class ContratacaoHospedagem : ContentPage
{
    public ContratacaoHospedagem()
    {
        BindingContext = new ContratacaoHospedagemViewModel();
        InitializeComponent();
    }


}