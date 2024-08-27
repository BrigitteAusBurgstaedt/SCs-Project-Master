using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using CommunityToolkit.Maui.Core.Extensions;
using CommunityToolkit.Mvvm.Input;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;

namespace SCsProjectMaster.Source.Models.ViewModels;

internal partial class MainViewModel : ObservableObject
{
    public MessageViewModel Status { get; }
    public MessageViewModel Error { get; }

    [ObservableProperty]
    private ObservableCollection<Project> _projects;

    [ObservableProperty]
    private Project _selectedProject = new();

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
            db.Update(SelectedProject);
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
            Project project = db.Projects.Include(p => p.EmployeeLogins).First(p => p.Id == SelectedProject.Id);
            List<Employee> teamMembers = new();
            foreach (Employee teamMember in project.EmployeeLogins)
            {
                teamMembers.Add(db.Employees.Include(e => e.ProjectsNavigation).First(e => e.Login == teamMember.Login));
            }
            project.EmployeeLogins.Clear();
            for (int i = 0; i < teamMembers.Count; i++)
            {
                teamMembers[i].ProjectsNavigation.Remove(teamMembers[i].ProjectsNavigation.First(p => p.Id == project.Id));
            }

            db.Update(project);
            db.UpdateRange(teamMembers);

            db.Remove(project);

            // db.ChangeTracker.DetectChanges();
            // Debug.WriteLine(db.ChangeTracker.DebugView.LongView);
            db.SaveChanges();
            Status.Message = "Projekt gelöscht";
        }
        catch (Exception)
        {
            Error.Message = "Fehler beim Zugriff auf die Datenbank. Internetverbindung überprüfen!";
            throw;
        }
        LoadProjects();
    }

    partial void OnSelectedProjectChanged(Project value)
    {
        HasProject = (value.Name != "");
    }

}
