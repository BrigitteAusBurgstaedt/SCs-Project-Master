using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core.Extensions;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace SCsProjectMaster.Source.Models.ViewModels;

internal partial class AddInvoiceViewModel : ObservableObject
{
    [ObservableProperty]
    private Invoice _invoice = new();
    [ObservableProperty]
    private InvoiceItem[] _invoiceItems = new InvoiceItem[7];
    [ObservableProperty]
    private ObservableCollection<Project> _projects;

    public AddInvoiceViewModel()
    {
        Load();
    }
    private void Load()
    {
        for (int i = 0; i < InvoiceItems.Length; i++)
        {
            InvoiceItems[i] = new InvoiceItem();
        }

        using DatabaseContext db = new();
        try
        {
            Projects = db.Projects.ToObservableCollection();
        }
        catch (Exception)
        {
            Toast.Make("Fehler: Zugriff auf die Datenbank nicht möglich. Internetverbindung überprüfen.").Show();
        }
    }

    [RelayCommand]
    private async Task CreateInvoice()
    {
        for (int i = 0; i < InvoiceItems.Length; i++)
        {
            if (InvoiceItems[i].Position != 0) // TODO überprüfen
            {
                Invoice.InvoiceItems.Add(InvoiceItems[i]);
            }
        }

        using DatabaseContext db = new();
        try
        {
            db.Projects.Attach(Invoice.Project);
            db.Add(Invoice);
            db.ChangeTracker.DetectChanges();
            Debug.WriteLine(db.ChangeTracker.DebugView.LongView);
            db.SaveChanges();
            await Toast.Make("Info: Rechnung in Datenbank gespeichert.").Show();
        }
        catch (Exception)
        {
            await Toast.Make("Fehler: Zugriff auf die Datenbank nicht möglich. Internetverbindung überprüfen.").Show();
        }

        try
        {
            var result = await FilePicker.Default.PickAsync();
            if (result != null)
            {
                if (result.FileName.EndsWith("xlsx", StringComparison.OrdinalIgnoreCase))
                {
                    Invoice.Create(result.FullPath);
                    await Toast.Make("Info: Rechnung erfolgreich erstellt.").Show();
                    return;
                }
                await Toast.Make("Fehler: Falsches DateiformatBitte wählen sie eine XLSX-Datei.").Show();
            }
            await Toast.Make("Fehler: Keine Datei ausgewählt.").Show();
        }
        catch (Exception)
        {
            await Toast.Make("Fehler: Datei konnte nicht gelesen werden. Bitte erneut versuchen.").Show();
        }
    }
}
