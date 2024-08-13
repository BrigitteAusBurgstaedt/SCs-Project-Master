using CommunityToolkit.Maui.Core.Extensions;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace SCsProjectMaster.Source.Models.ViewModels;

internal partial class ShowCustomersViewModel : ObservableObject
{
    [ObservableProperty]
    private ObservableCollection<Customer> _customers;

    public ShowCustomersViewModel()
    {
        using DatabaseContext db = new();
        _customers = db.Customers.ToObservableCollection();
    }

    [RelayCommand]
    private void SaveChanges()
    {
        using DatabaseContext db = new();
        db.Customers.UpdateRange(Customers);
        db.SaveChanges();
    }
}

