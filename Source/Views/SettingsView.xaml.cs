using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Storage;
using SCsProjectMaster.Source.Models;
using SCsProjectMaster.Source.Models.ViewModels;
using System.Text;

namespace SCsProjectMaster.Source.Views;

public partial class SettingsView : ContentPage
{
	public SettingsView()
	{
		InitializeComponent();
        BindingContext = new SettingsViewModel();
	}

    private async void OnAddCategorieClicked(object sender, EventArgs e)
    {
        var result = await FolderPicker.Default.PickAsync(CancellationToken.None);
        if (result.IsSuccessful)
        {
            string[] folders = result.Folder.Path.Split('/');
            StringBuilder sb = new(folders[0]);
            for (int i = 1; i < folders.Length - 1; i++)
            {
                sb.Append(folders[i]);
            }
            Configuration.Instance.CategoriePathsAndNames.Add((sb.ToString(), folders[^1]));
        }
        else
        {
            await Toast.Make($"The folder was not picked with error: {result.Exception.Message}").Show(CancellationToken.None);
        }

    }

    private async void OnSaveAsJsonClicked(object sender, EventArgs e)
    {
        var result = await FolderPicker.Default.PickAsync(CancellationToken.None);
        if (result.IsSuccessful)
        {
            string filePath = result.Folder.Path + Configuration.Instance.FileName;
            await File.WriteAllTextAsync(filePath, Preferences.Get("ConfigurationJson", "Unknown"));
        }
        else
        {
            await Toast.Make($"The folder was not picked with error: {result.Exception.Message}").Show(CancellationToken.None);
        }
    }
}