using SCsProjectMaster.Source.Models.ViewModels;

namespace SCsProjectMaster.Source.Views;

public partial class LoginView : ContentPage
{
	public LoginView()
	{
		InitializeComponent();
		LoginViewModel viewModel = new LoginViewModel();
        viewModel.LoginCompleted += ViewModel_LoginCompleted;
		BindingContext = viewModel;
}

    private async void ViewModel_LoginCompleted(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(MainView));
    }
}
