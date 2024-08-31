using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SCsProjectMaster.Source.Logic;

namespace SCsProjectMaster.Source.Models.ViewModels;

internal partial class AddEmployeeViewModel : ObservableObject
{
    [ObservableProperty]
    private Address _address = new();

    [ObservableProperty]
    private Employee _employee = new();

    [ObservableProperty]
    private string _password;

    public AddEmployeeViewModel() { }

    [RelayCommand]
    private async Task SaveChanges()
    {
        Employee.Address = Address;
        Employee.PasswordHash = LoginTool.HashPassword(Password, out string salt);
        Employee.PasswordSalt = salt;

        System.Diagnostics.Debug.WriteLine("Hash: {0} Salt: {1}", Employee.PasswordHash, Employee.PasswordSalt);

        if (Employee.Login == "")
        {
            await Toast.Make("Fehler: Login darf nicht leer sein und muss einzigartig sein.").Show();
            return;
        }

        if (Employee.PhoneNumber == "")
        {
            await Toast.Make("Fehler: Telefonnummer darf nicht leer sein.").Show();
            return;
        }

        using DatabaseContext db = new();
        try
        {
            db.Employees.Add(Employee);
            db.SaveChanges();
            await Toast.Make("Info: Mitarbeiter hinzugefügt.").Show();
            Address = new Address();
            Employee = new Employee();
            Password = "";
        }
        catch (Exception)
        {
            await Toast.Make("Fehler: Zugriff auf die Datenbank nicht möglich. Internetverbindung überprüfen.").Show();
        }

    }
}
