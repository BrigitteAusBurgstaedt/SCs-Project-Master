using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Storage;
using SCsProjectMaster.Source.Models;
using SCsProjectMaster.Source.Models.ViewModels;
using System.Text;

namespace SCsProjectMaster.Source.Views;

public partial class SettingsView : ContentPage
{
	public SettingsView()
	{
		InitializeComponent();
        BindingContext = new SettingsViewModel();
	}
}