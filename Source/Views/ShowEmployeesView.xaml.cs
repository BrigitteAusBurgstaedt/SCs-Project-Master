using SCsProjectMaster.Source.Models.ViewModels;

namespace SCsProjectMaster.Source.Views;

public partial class ShowEmployeesView : ContentPage
{

	public ShowEmployeesView()
	{
        InitializeComponent();
		BindingContext = new ShowEmployeesViewModel();
	}
}