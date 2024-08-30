using SCsProjectMaster.Source.Models.ViewModels;

namespace SCsProjectMaster.Source.Views;

public partial class MainView : ContentPage
{
    public MainView()
    {
        InitializeComponent();
        BindingContext = new MainViewModel();
        Appearing += ((MainViewModel)BindingContext).MainView_Appearing;
    }

    private async void OnSettingsClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(MainView) + "/" + nameof(SettingsView));
    }

    private async void OnAddCustomerClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(MainView) + "/" + nameof(AddCustomerView));
    }
    private async void OnAddEmployeeClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(MainView) + "/" + nameof(AddEmployeeView));
    }

    private async void OnShowCustomersClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(MainView) + "/" + nameof(ShowCustomersView));
    }

    private async void OnShowEmployeesClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(MainView) + "/" + nameof(ShowEmployeesView));
    }

    private async void OnAddProjectClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(MainView) + "/" + nameof(AddProjectView));
    }

    private async void NotImplementedWarning(object sender, EventArgs e)
    {
        await DisplayAlert("Obacht!", "Diese Funktion wurde nocht nicht implementiert", "OK");
    }
}
