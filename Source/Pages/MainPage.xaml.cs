using SCs_Project_Master.Source.Logic;
using SCs_Project_Master.Source.Models.ViewModels;

namespace SCs_Project_Master;

public partial class MainPage : ContentPage
{
    int count = 0;

    public MainPage()
    {
        InitializeComponent();

        var mainPaigeViewModel = new MainPaigeViewModel
        {
            Project = new DatabaseContext().Projects.OrderBy(b => b.Id).First()
        };

        BindingContext = mainPaigeViewModel;
    }
}
