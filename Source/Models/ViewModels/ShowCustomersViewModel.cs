using CommunityToolkit.Maui.Core.Extensions;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace SCsProjectMaster.Source.Models.ViewModels;

internal partial class ShowCustomersViewModel : ObservableObject
{
    public MessageViewModel Status { get; }
    public MessageViewModel Error { get; }

    [ObservableProperty]
    private ObservableCollection<Customer> _customers;
    [ObservableProperty]
    private Customer _selectedCustomer;

    public ShowCustomersViewModel()
    {
        Status = new MessageViewModel();
        Error = new MessageViewModel();

        using DatabaseContext db = new();
        _customers = db.Customers.ToObservableCollection();
    }

    [RelayCommand]
    private void SaveChanges()
    {
        Status.Message = "";
        Error.Message = "";

        using DatabaseContext db = new();
        try
        {
            db.Customers.UpdateRange(Customers);
            db.SaveChanges();
            Status.Message = "Änderungen gespeichert";
        }
        catch (Exception)
        {
            Error.Message = "Fehler beim Zugriff auf die Datenbank. Internetverbindung überprüfen!";
        }
    }

    [RelayCommand]
    private void DeleteCustomer()
    {
        Status.Message = "";
        Error.Message = "";
        // TODO: ausgewählten Kunden löschen
    }
}

