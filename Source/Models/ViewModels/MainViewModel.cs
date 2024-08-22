using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using CommunityToolkit.Maui.Core.Extensions;
using CommunityToolkit.Mvvm.Input;

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

        using DatabaseContext db = new();
        try
        {
            Projects = db.Projects.ToObservableCollection();
            Status.Message = "Online";
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
    }

    partial void OnProjectChanged(Project value)
    {
        HasProject = (value.Name != "");
    }

}
