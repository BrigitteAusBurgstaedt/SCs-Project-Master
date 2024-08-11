using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using CommunityToolkit.Maui.Core.Extensions;

namespace SCsProjectMaster.Source.Models.ViewModels;

internal partial class MainViewModel : ObservableObject
{
    [ObservableProperty]
    private ObservableCollection<Project> _projects;

    [ObservableProperty]
    private Project _project = new();

    public MainViewModel() 
    {
        using DatabaseContext db = new();
        try
        {
            Projects = db.Projects.ToObservableCollection();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            throw;
        }
    }

}
