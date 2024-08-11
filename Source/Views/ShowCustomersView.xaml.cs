using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Storage;
using SCsProjectMaster.Source.Models.ViewModels;
using System.Text;

namespace SCsProjectMaster.Source.Views;

public partial class ShowCustomersView : ContentPage
{

	public ShowCustomersView()
	{
        InitializeComponent();
		BindingContext = new ShowCustomersViewModel();
	}
}