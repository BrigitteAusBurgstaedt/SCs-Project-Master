using SCsProjectMaster.Source.Models.ViewModels;
using SCsProjectMaster.Source.Models;
using System.Collections.ObjectModel;

namespace SCsProjectMaster;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();

        var test = new ObservableCollection<Project>
        {
            new DatabaseContext().Projects.OrderBy(b => b.Id).First()
        };

        var mainPaigeViewModel = new MainPaigeViewModel
        {
            Projects = test
        };

        BindingContext = mainPaigeViewModel;

    }

    private async void NotImplementedWarning(object sender, EventArgs e)
    {
        await DisplayAlert("Obacht!", "Diese Funktion wurde nocht nicht implementiert", "OK");
    }
}
