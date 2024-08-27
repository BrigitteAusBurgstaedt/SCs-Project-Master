using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;


namespace SCsProjectMaster.Source.Models.ViewModels;

internal partial class AddProjectViewModel : ObservableObject
{
    public MessageViewModel Status { get; }
    public MessageViewModel Error { get; }

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
    private Configuration _configuration = Configuration.Instance;

    [ObservableProperty]
    private IList<string> _keys = Configuration.Instance.Categories().ToList();

    public AddProjectViewModel()
    {
        Status = new MessageViewModel();
        Error = new MessageViewModel();

        using DatabaseContext db = new();
        try
        {
            Customers = db.Customers.ToList();
            Employees = db.Employees.ToList();
        }
        catch (Exception)
        {
            Error.Message = "Fehler beim Zugriff auf die Datenbank. Internetverbindung überprüfen!";
        }
    }

    [RelayCommand]
    private void Add()
    {
        Error.Message = "";
        Status.Message = "";

        if (Project.Name == "")
        {
            Error.Message = "Fehler: Projektname darf nicht leer sein.";
            return;
        }

        if (SelectedCustomer == null || SelectedEmployee == null)
        {
            Error.Message = "Fehler: Eingabe überprüfen. Kunde und Ansprechpartner dürfen nicht leer sein.";
            return;
        }

        using DatabaseContext db = new(new DbContextOptionsBuilder<DatabaseContext>().EnableSensitiveDataLogging(true).Options);
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
            Status.Message = "Projekt hinzugefügt";
            Project = new Project();
        }
        catch (Exception)
        {
            Error.Message = "Fehler: Zugriff auf die Datenbank nicht möglich. Internetverbindung überprüfen.";
        }
    }
}
