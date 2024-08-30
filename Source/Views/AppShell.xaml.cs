using SCsProjectMaster.Source.Views;

namespace SCsProjectMaster;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        Routing.RegisterRoute(nameof(SettingsView), typeof(SettingsView));
        Routing.RegisterRoute(nameof(AddCustomerView), typeof(AddCustomerView));
        Routing.RegisterRoute(nameof(AddEmployeeView), typeof(AddEmployeeView));
        Routing.RegisterRoute(nameof(AddProjectView), typeof(AddProjectView));
        Routing.RegisterRoute(nameof(ShowCustomersView), typeof(ShowCustomersView));
        Routing.RegisterRoute(nameof(ShowEmployeesView), typeof(ShowEmployeesView));
    }
}
