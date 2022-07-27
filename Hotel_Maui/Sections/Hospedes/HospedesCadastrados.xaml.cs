using Hotel_Maui.Sections.Hospedes;

namespace Hotel_Maui.View;

public partial class HospedesCadastrados : ContentPage
{
    public HospedesCadastrados()
    {
        BindingContext = new HospedesCadastradosPageViewModel();
        InitializeComponent();
    }
    protected override void OnAppearing()
    {
        var vm = (HospedesCadastradosPageViewModel)BindingContext;
        vm.AtualizarLista.Execute(null);
    }



}