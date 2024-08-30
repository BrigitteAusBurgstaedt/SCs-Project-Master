using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using CommunityToolkit.Maui.Core.Extensions;
using CommunityToolkit.Mvvm.Input;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;

namespace SCsProjectMaster.Source.Models.ViewModels;

internal partial class MainViewModel : ObservableObject
{
    [ObservableProperty]
    private ObservableCollection<Project> _projects;

    [ObservableProperty]
    private Project _selectedProject = new();

    [ObservableProperty]
    private bool _hasProject = false;

    [ObservableProperty]
    private FolderPreset _selectedFolderPreset = new();

    [ObservableProperty]
    private Configuration _configuration;

    public MainViewModel()
    {
        Load();
    }

    public void MainView_Appearing(object sender, EventArgs e)
    {
        Load();
    }

    private void Load()
    {
        Configuration = Configuration.GetConfiguration();
        using DatabaseContext db = new();
        try
        {
            Projects = db.Projects.ToObservableCollection();
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
            db.Update(SelectedProject);
            db.SaveChanges();
            await Toast.Make("Info: Änderungen gespeichert").Show();
        }
        catch (Exception)
        {
            await Toast.Make("Fehler: Zugriff auf die Datenbank nicht möglich. Internetverbindung überprüfen.").Show();
        }
        Load();
    }

    [RelayCommand]
    private async Task DeleteProject()
    {
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
            await Toast.Make("Info: Projekt gelöscht").Show();
        }
        catch (Exception)
        {
            await Toast.Make("Fehler: Zugriff auf die Datenbank nicht möglich. Internetverbindung überprüfen.").Show();
            throw;
        }
        Load();
    }

    [RelayCommand]
    private async Task CreateFolderStructure()
    {
        try
        {
            SelectedProject.CreateFolderStructure(SelectedFolderPreset.Root);
            await Toast.Make("Info: Ordnerstruktur erstellt.").Show();
        }
        catch (Exception)
        {
            await Toast.Make("Fehler: Ordnerstruktur konnte nicht erstellt werden.").Show();
        }
    }

    [RelayCommand]
    private async Task Archive()
    {
        try
        {
            SelectedProject.Archive();
            await Toast.Make("Info: Projekt wurde archiviert.").Show();
        }
        catch (Exception) {
            await Toast.Make("Fehler: Projekt konnte nicht archiviert werden").Show();
        }
    }

    partial void OnSelectedProjectChanged(Project value)
    {
        HasProject = (value.Name != "");
    }

}
