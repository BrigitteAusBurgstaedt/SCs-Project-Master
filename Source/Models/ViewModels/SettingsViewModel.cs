using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SCsProjectMaster.Source.Models.ViewModels;

internal partial class SettingsViewModel : ObservableObject
{
    [ObservableProperty]
    private string _configurationJson;
    [ObservableProperty]
    private Configuration _configuration;

    public SettingsViewModel() 
    {
        Configuration = Configuration.Instance;
    }

    [RelayCommand]
    private void SaveChanges()
    {
        Configuration.Instance = Configuration;
        var jsonOptions = new JsonSerializerOptions { WriteIndented = true };
        string jsonString = JsonSerializer.Serialize(Configuration.Instance, jsonOptions);
        Preferences.Default.Set("ConfigurationJson", jsonString);
    }

    [RelayCommand]
    private void LoadConfigurationJson() 
    {
        try
        {
            Configuration = JsonSerializer.Deserialize<Configuration>(ConfigurationJson);
            SaveChanges();
        } catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            throw; // TODO: Alert
        }
        
    }
}
