using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;


namespace SCsProjectMaster.Source.Models.ViewModels;

internal partial class AddProjectViewModel : ObservableObject
{
    [ObservableProperty]
    private Project _project = new();

    [ObservableProperty]
    private IList<Customer> _customers;

    [ObservableProperty]
    private Customer _selectedCustomer;

    [ObservableProperty]
    private IList<Employee> _employees;

    [ObservableProperty]
    private Employee _selectedEmployee;

    [ObservableProperty]
    private IList<Employee> _selectedEmployees;

    [ObservableProperty]
    private Configuration _configuration = Configuration.GetConfiguration();

    [ObservableProperty]
    private IList<string> _keys = Configuration.GetConfiguration().Categories().ToList();

    public AddProjectViewModel()
    {
        using DatabaseContext db = new();
        try
        {
            Customers = db.Customers.ToList();
            Employees = db.Employees.ToList();
        }
        catch (Exception)
        {
            Toast.Make("Fehler: Zugriff auf die Datenbank nicht möglich. Internetverbindung überprüfen.").Show();
        }
    }

    [RelayCommand]
    private async Task Add()
    {
        if (Project.Name == "")
        {
            await Toast.Make("Fehler: Projektname ist leer. Geben sie dem Projekt einen Namen.").Show();
            return;
        }

        if (SelectedCustomer == null || SelectedEmployee == null)
        {
            await Toast.Make("Fehler: Kunde bzw. Ansprechpartner sind leer. Eingabe überprüfen.").Show();
            return;
        }

        using DatabaseContext db = new();
        try
        {
            Project.CustomerId = SelectedCustomer.Id;
            Project.ContactPerson = SelectedEmployee.Login;
            db.Employees.Attach(SelectedEmployee);
            Project.EmployeeLogins.Add(SelectedEmployee); // Ansprechpartner muss im Projektteam sein
            if (SelectedEmployees != null)
            {
                foreach (Employee teamMember in SelectedEmployees)
                {
                    if (!teamMember.Equals(SelectedEmployee))
                    {
                        db.Employees.Attach(teamMember);
                        Project.EmployeeLogins.Add(teamMember);
                    }
                }
            }
            db.Projects.Add(Project);
            db.SaveChanges();
            await Toast.Make("Info: Projekt hinzugefügt").Show();
            Project = new Project();
        }
        catch (Exception)
        {
            await Toast.Make("Fehler: Zugriff auf die Datenbank nicht möglich. Internetverbindung überprüfen.").Show();
        }
    }
}
