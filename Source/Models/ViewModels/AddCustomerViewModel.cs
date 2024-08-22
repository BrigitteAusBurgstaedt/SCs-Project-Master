using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace SCsProjectMaster.Source.Models.ViewModels;

internal partial class AddCustomerViewModel : ObservableObject
{
    public MessageViewModel Status { get; }
    public MessageViewModel Error { get; }

    [ObservableProperty]
    private Address _address = new();
    [ObservableProperty]
    private Customer _customer = new();

    public AddCustomerViewModel()
    {
        Error = new MessageViewModel();
        Status = new MessageViewModel();
    }

    [RelayCommand]
    private void SaveChanges()
    {
        Error.Message = "";
        Status.Message = "";

        using DatabaseContext db = new();
        try
        {
            Customer.Address = Address;
            db.Customers.Add(Customer);
            db.SaveChanges();
            Status.Message = "Kunde hinzugefügt";
        }
        catch (Exception)
        {
            Error.Message = "Fehler: Zugriff auf die Datenbank nicht möglich. Internetverbindung überprüfen.";
        }

    }
}
