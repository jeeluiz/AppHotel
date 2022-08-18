using Hotel_Maui.Sections.Hospedes;

namespace Hotel_Maui.Sections.Hospedagem;

public partial class ReservaCadastrada : ContentPage
{
    public ReservaCadastrada()
    {
        BindingContext = new ReservaCadastradaPageViewModel();
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        var vm = (ReservaCadastradaPageViewModel)BindingContext;
        vm.AtualizarLista.Execute(null);
    }
}