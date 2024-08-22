using SCsProjectMaster.Source.Models.ViewModels;

namespace SCsProjectMaster.Source.Views;

public partial class AddEmployeeView : ContentPage
{
	public AddEmployeeView()
	{
		InitializeComponent();
        BindingContext = new AddEmployeeViewModel();
	}
}