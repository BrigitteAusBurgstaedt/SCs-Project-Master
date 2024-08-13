using CommunityToolkit.Mvvm.ComponentModel;

namespace SCsProjectMaster.Source.Models.ViewModels;

internal partial class MessageViewModel : ObservableObject
{
    [ObservableProperty]
    private string _message;

    [ObservableProperty]
    private bool _hasMessage = false;

    partial void OnMessageChanged(string value)
    {
        HasMessage = !string.IsNullOrEmpty(value);
    }
}
