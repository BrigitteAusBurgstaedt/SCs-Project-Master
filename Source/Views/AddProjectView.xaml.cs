using SCsProjectMaster.Source.Models.ViewModels;

namespace SCsProjectMaster.Source.Views;

public partial class AddProjectView : ContentPage
{
	public AddProjectView()
	{
		InitializeComponent();
		BindingContext = new AddProjectViewModel();
    }
}