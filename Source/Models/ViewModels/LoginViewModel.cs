using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SCsProjectMaster.Source.Logic;

namespace SCsProjectMaster.Source.Models.ViewModels;

internal partial class LoginViewModel : ObservableObject
{
    public event EventHandler LoginCompleted;
    [ObservableProperty]
    private string _loginString;
    [ObservableProperty]
    private string _password;

    public LoginViewModel() { }

    [RelayCommand]
    private async Task Login()
    {
        using DatabaseContext db = new();
        try
        {
            Employee user = db.Employees.First(e => e.Login == LoginString);
            if (LoginTool.VerifyHashedPassword(user.PasswordHash, Password, user.PasswordSalt))
            {
                // Configuration config = Configuration.GetConfiguration();
                // config.Login = LoginString;
                // Configuration.SetConfiguration(config);
                await Toast.Make("Info: Erfolgreich angemeldet.").Show();
                LoginCompleted?.Invoke(this, EventArgs.Empty);
                return;
            }
            await Toast.Make("Fehler: Anmeldedaten nicht korrekt.").Show();
        }
        catch (Exception)
        {
            await Toast.Make("Fehler: Zugriff auf die Datenbank nicht möglich. Internetverbindung überprüfen.").Show();
        }

    }
}
