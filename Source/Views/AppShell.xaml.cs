using SCsProjectMaster.Source.Views;

namespace SCsProjectMaster;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        Routing.RegisterRoute(nameof(SettingsView), typeof(SettingsView));
        Routing.RegisterRoute(nameof(AddCustomerView), typeof(AddCustomerView));
        Routing.RegisterRoute(nameof(ShowCustomersView), typeof(ShowCustomersView));
    }
}
