using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace SaisApp.Models;

public class ProgramsList : ObservableObject
{
    [JsonProperty("programmeId")]
    public int ProgrammeId { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; } = null!;

    private bool _isSelected;
    public bool IsSelected
    {
        get => _isSelected;
        set => SetProperty(ref _isSelected, value); // This raises INotifyPropertyChanged
    }
}