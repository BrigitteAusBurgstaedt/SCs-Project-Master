using SCsProjectMaster.Source.Models.ViewModels;

namespace SCsProjectMaster.Source.Views;

public partial class AddInvoiceView : ContentPage
{
	public AddInvoiceView()
	{
		InitializeComponent();
        BindingContext = new AddInvoiceViewModel();
	}
}