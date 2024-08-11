using SCsProjectMaster.Source.Models.ViewModels;

namespace SCsProjectMaster.Source.Views;

public partial class MainView : ContentPage
{
    public MainView()
    {
        InitializeComponent();
        BindingContext = new MainViewModel();
    }

    private async void OnSettingsClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(SettingsView));
    }

    private async void OnAddCustomerClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(AddCustomerView));
    }

    private async void OnShowCustomersClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(ShowCustomersView));
    }

    private async void NotImplementedWarning(object sender, EventArgs e)
    {
        await DisplayAlert("Obacht!", "Diese Funktion wurde nocht nicht implementiert", "OK");
    }
}
