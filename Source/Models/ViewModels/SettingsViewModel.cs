using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Storage;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;


namespace SCsProjectMaster.Source.Models.ViewModels;

internal partial class SettingsViewModel : ObservableObject
{
    [ObservableProperty]
    private Configuration _configuration;

    public SettingsViewModel() 
    {
        Configuration = Configuration.GetConfiguration();
    }

    [RelayCommand]
    private void SaveChanges()
    {
        Configuration.SetConfiguration(Configuration);
    }

    [RelayCommand]
    private async Task DownloadConfiguration() 
    {
        var result = await FolderPicker.Default.PickAsync(CancellationToken.None);
        if (result.IsSuccessful)
        {
            Configuration.SaveConfiguration(result.Folder.Path);
            await Toast.Make("Konfiguration gespeichert.").Show();
            return;
        }
        await Toast.Make("Konfiguration nicht gespeichert.").Show();
    }

    [RelayCommand]
    private async Task LoadConfiguration() 
    {
        try
        {
            var result = await FilePicker.Default.PickAsync();
            if (result != null)
            {
                if (result.FileName.EndsWith("xml", StringComparison.OrdinalIgnoreCase))
                {
                    Configuration.LoadConfiguration(result.FullPath);
                    await Toast.Make("Konfiguration erfolgreich geladen.").Show();
                    Configuration = Configuration.GetConfiguration();
                    return;
                }
                await Toast.Make("Bitte wählen sie eine XML-Datei.").Show();
            }
            await Toast.Make("Konfiguration nicht geladen.").Show();
        }
        catch (Exception)
        {
            await Toast.Make("Fehler: Datei konnte nicht gelesen werden. Bitte erneut versuchen.").Show();
        }
        
    }
}
