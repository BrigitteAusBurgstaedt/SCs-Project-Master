using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core.Extensions;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace SCsProjectMaster.Source.Models.ViewModels;

internal partial class ShowCustomersViewModel : ObservableObject
{
    [ObservableProperty]
    private ObservableCollection<Customer> _customers;
    [ObservableProperty]
    private Customer _selectedCustomer;

    public ShowCustomersViewModel()
    {
        Load();
    }

    private void Load()
    {
        using DatabaseContext db = new();
        try
        {
            Customers = db.Customers.Include(c => c.Address).ToObservableCollection();
        }
        catch (Exception)
        {
            Toast.Make("Fehler: Zugriff auf die Datenbank nicht möglich. Internetverbindung überprüfen.").Show();
        }
    }

    [RelayCommand]
    private async Task SaveChanges()
    {
        using DatabaseContext db = new();
        try
        {
            db.Customers.UpdateRange(Customers);
            db.SaveChanges();
            await Toast.Make("Info: Änderungen gespeichert.").Show();
        }
        catch (Exception)
        {
            await Toast.Make("Fehler: Zugriff auf die Datenbank nicht möglich. Internetverbindung überprüfen.").Show();
        }
        Load();
    }

    [RelayCommand]
    private async Task DeleteCustomer()
    {
        using DatabaseContext db = new();
        try
        {
            Customer customer = db.Customers.Include(c => c.Address).First(c => c.Id == SelectedCustomer.Id);
            Address address = db.Addresses.Include(a => a.Customers).First(a => a.Id == customer.Address.Id);
            // TODO handle Address
            customer.Address = null;
            db.Update(customer);
            db.Remove(customer);
            db.ChangeTracker.DetectChanges();
            Debug.WriteLine(db.ChangeTracker.DebugView.LongView);
            db.SaveChanges();
            await Toast.Make("Info: Kunde gelöscht").Show();
        }
        catch (Exception)
        {
            await Toast.Make("Fehler: Zugriff auf die Datenbank nicht möglich. Internetverbindung überprüfen.").Show();
            // TODO Fehler: Kunde an Projekt beteiligt
        }
        Load();
    }
}

