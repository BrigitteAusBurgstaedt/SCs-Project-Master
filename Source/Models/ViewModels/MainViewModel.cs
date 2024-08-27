using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using CommunityToolkit.Maui.Core.Extensions;
using CommunityToolkit.Mvvm.Input;
using System.Diagnostics;

namespace SCsProjectMaster.Source.Models.ViewModels;

internal partial class MainViewModel : ObservableObject
{
    public MessageViewModel Status { get; }
    public MessageViewModel Error { get; }

    [ObservableProperty]
    private ObservableCollection<Project> _projects;

    [ObservableProperty]
    private Project _project = new();

    [ObservableProperty]
    private bool _hasProject = false;

    public MainViewModel() 
    {
        Status = new MessageViewModel();
        Error = new MessageViewModel();

        LoadProjects();
    }

    public void MainView_Appearing(object sender, EventArgs e)
    {
        LoadProjects();
    }

    private void LoadProjects()
    {
        using DatabaseContext db = new();
        try
        {
            Projects = db.Projects.ToObservableCollection();
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
            db.Update(Project);
            db.SaveChanges();
            Status.Message = "Änderung gespeichert";
        }
        catch (Exception)
        {
            Error.Message = "Fehler beim Zugriff auf die Datenbank. Internetverbindung überprüfen!";
        }   
        LoadProjects();
    }

    [RelayCommand]
    private void DeleteProject()
    {
        Status.Message = "";
        Error.Message = "";

        using DatabaseContext db = new();
        try
        {
            // Fremdschlüsselbeziegung auflösen
            Project p = Project;
            if (p.EmployeeLogins.Count != 0) 
            {
                Employee e = p.EmployeeLogins.First();
                db.Attach(p);
                db.Attach(e);

                p.EmployeeLogins.Remove(e);
                e.ProjectsNavigation.Remove(p);
            }
            Employee em = db.Employees.First(e => e.Login == "jpfeifer");
            /*
            em.ProjectsNavigation.Clear();
            db.Employees.Update(em);
            db.Remove(p);
            */
            Dictionary<string, object> dict = new()
            {
                { "ProjectId", p.Id },
                { "EmployeeLogin", em.Login }
            };
            db.Attach(p);
            db.Attach(em);
            // db.Attach(dict);
            db.Remove(dict);
            db.ChangeTracker.DetectChanges();
            Debug.WriteLine(db.ChangeTracker.DebugView.LongView);
            // db.SaveChanges();
            Status.Message = "Projekt gelöscht";
        }
        catch (Exception)
        {
            Error.Message = "Fehler beim Zugriff auf die Datenbank. Internetverbindung überprüfen!";
            throw;
        }
        LoadProjects();
    }

    partial void OnProjectChanged(Project value)
    {
        HasProject = (value.Name != "");
    }

}
