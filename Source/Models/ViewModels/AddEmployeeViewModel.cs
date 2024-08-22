using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SCsProjectMaster.Source.Logic;

namespace SCsProjectMaster.Source.Models.ViewModels;

internal partial class AddEmployeeViewModel : ObservableObject
{
    public MessageViewModel Status { get; }
    public MessageViewModel Error { get; }

    [ObservableProperty]
    private Address _address = new();

    [ObservableProperty]
    private Employee _employee = new();

    [ObservableProperty]
    private string _password;

    public AddEmployeeViewModel()
    {
        Error = new MessageViewModel();
        Status = new MessageViewModel();
    }

    [RelayCommand]
    private void SaveChanges()
    {
        Error.Message = "";
        Status.Message = "";

        Employee.Address = Address;
        Employee.PasswordHash = LoginTool.HashPassword(Password, out string salt);
        Employee.PasswordSalt = salt;

        System.Diagnostics.Debug.WriteLine("Hash: {0} Salt: {1}", Employee.PasswordHash, Employee.PasswordSalt);

        if (Employee.Login == "")
        {
            Error.Message = "Fehler: Login darf nicht leer sein und muss einzigartig sein.";
            return;
        }

        if (Employee.PhoneNumber == "")
        {
            Error.Message = "Fehler: Telefonnummer darf nicht leer sein.";
            return;
        }

        using DatabaseContext db = new();
        try
        {
            db.Employees.Add(Employee);
            db.SaveChanges();
            Status.Message = "Mitarbeiter hinzugefügt";
        }
        catch (Exception)
        {
            Error.Message = "Fehler: Zugriff auf die Datenbank nicht möglich. Internetverbindung überprüfen.";
        }

    }
}
