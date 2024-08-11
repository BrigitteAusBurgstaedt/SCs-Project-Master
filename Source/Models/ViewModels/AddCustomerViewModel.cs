using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace SCsProjectMaster.Source.Models.ViewModels;

internal partial class AddCustomerViewModel : ObservableObject
{
    [ObservableProperty]
    private Address _address = new();
    [ObservableProperty]
    private Customer _customer = new();

    [RelayCommand]
    private void SaveChanges()
    {
        using DatabaseContext db = new();
        Customer.Address = Address;
        db.Customers.Add(Customer);
        db.SaveChanges();
    }
}
