using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core.Extensions;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;

namespace SCsProjectMaster.Source.Models.ViewModels;

internal partial class ShowEmployeesViewModel : ObservableObject
{
    [ObservableProperty]
    private ObservableCollection<Customer> _employees;
    [ObservableProperty]
    private Customer _selecteEmployee;

    public ShowEmployeesViewModel()
    {
        Load();
    }

    private void Load()
    {
        using DatabaseContext db = new();
        try
        {
            Employees = db.Employees.Include(c => c.Address).ToObservableCollection();
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
            db.Employees.UpdateRange(Employees);
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
            Employee employee = db.Employees.Include(c => c.Address).First(c => c.Login == SelectedEmployee.Id);
            Address address = db.Addresses.Include(a => a.Employees).First(a => a.Id == employee.Address.Id);
            // TODO handle Address
            employee.Address = null;
            db.Update(employee);
            db.Remove(employee);
            // db.ChangeTracker.DetectChanges();
            // Debug.WriteLine(db.ChangeTracker.DebugView.LongView);
            db.SaveChanges();
            await Toast.Make("Info: Mitarbeiter gelöscht").Show();
        }
        catch (Exception)
        {
            await Toast.Make("Fehler: Zugriff auf die Datenbank nicht möglich. Internetverbindung überprüfen.").Show();
            // TODO Fehler: Mitarbeiter an Projekt beteiligt
        }
        Load();
    }
}

