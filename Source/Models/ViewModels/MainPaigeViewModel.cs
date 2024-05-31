using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;

namespace SCsProjectMaster.Source.Models.ViewModels;

internal partial class MainPaigeViewModel : ObservableObject
{
    [ObservableProperty]
    private ObservableCollection<Project> _projects = new();

    [ObservableProperty]
    private Project _project = new();

}
