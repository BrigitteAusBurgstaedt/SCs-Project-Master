using SCsProjectMaster.Source.Models;
using SCsProjectMaster.Source.Models.ViewModels;

namespace SCsProjectMaster.Source.Views;

public partial class AddCustomerView : ContentPage
{
	public AddCustomerView()
	{
		InitializeComponent();
        BindingContext = new AddCustomerViewModel();
	}
}