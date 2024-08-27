using CommunityToolkit.Maui.Core.Extensions;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using System.Collections.ObjectModel;
using System.Diagnostics;

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

        LoadCustomers();
    }

    private void LoadCustomers()
    {
        using DatabaseContext db = new();
        try
        {
            Customers = db.Customers.Include(c => c.Address).ToObservableCollection();
        }
        catch (Exception)
        {
            Error.Message = "Fehler beim Zugriff auf die Datenbank. Internetverbindung überprüfen!";
        }
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
        LoadCustomers();
    }

    [RelayCommand]
    private void DeleteCustomer()
    {
        Status.Message = "";
        Error.Message = "";

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
            Status.Message = "Kunde gelöscht";
        }
        catch (Exception)
        {
            Error.Message = "Fehler beim löschen. Kunde evtl. Teil eines Projektes oder Internetverbindung überprüfen!";
        }
        LoadCustomers();
    }
}

