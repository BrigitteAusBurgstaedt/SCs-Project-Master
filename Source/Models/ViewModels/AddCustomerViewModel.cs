using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace SCsProjectMaster.Source.Models.ViewModels;

internal partial class AddCustomerViewModel : ObservableObject
{
    [ObservableProperty]
    private Address _address = new();
    [ObservableProperty]
    private Customer _customer = new();

    public AddCustomerViewModel() { }

    [RelayCommand]
    private async Task SaveChanges()
    {
        using DatabaseContext db = new();
        try
        {
            Customer.Address = Address;
            db.Customers.Add(Customer);
            db.SaveChanges();
            await Toast.Make("Info: Kunde hinzugefügt.").Show();
        }
        catch (Exception)
        {
            await Toast.Make("Fehler: Zugriff auf die Datenbank nicht möglich. Internetverbindung überprüfen.").Show();
        }

    }
}
