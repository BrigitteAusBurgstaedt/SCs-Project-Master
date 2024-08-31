using SCsProjectMaster.Source.Views;

namespace SCsProjectMaster;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        Routing.RegisterRoute(nameof(MainView), typeof(MainView));
        Routing.RegisterRoute(nameof(MainView) + "/" + nameof(SettingsView), typeof(SettingsView));
        Routing.RegisterRoute(nameof(MainView) + "/" + nameof(AddCustomerView), typeof(AddCustomerView));
        Routing.RegisterRoute(nameof(MainView) + "/" + nameof(AddEmployeeView), typeof(AddEmployeeView));
        Routing.RegisterRoute(nameof(MainView) + "/" + nameof(AddProjectView), typeof(AddProjectView));
        Routing.RegisterRoute(nameof(MainView) + "/" + nameof(AddInvoiceView), typeof(AddInvoiceView));
        Routing.RegisterRoute(nameof(MainView) + "/" + nameof(ShowCustomersView), typeof(ShowCustomersView));
        Routing.RegisterRoute(nameof(MainView) + "/" + nameof(ShowEmployeesView), typeof(ShowEmployeesView));
    }
}
