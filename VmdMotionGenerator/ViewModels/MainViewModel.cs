using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using VmdMotionGenerator.Core.Models;
using VmdMotionGenerator.Helpers;

namespace VmdMotionGenerator.ViewModels;

public partial class MainViewModel : ObservableRecipient
{
    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(SaveVmdCommand))]
    private string _npyPath;
    [ObservableProperty]
    private int _repetitionsMaximum;
    [ObservableProperty]
    private int _targetRepetition;

    public MainViewModel()
    {
        _npyPath = "";
    }

    [RelayCommand]
    private async void ReadNpy()
    {
        var npy = await StorageHelper.PickSingleFileAsync(".npy");
        if(npy is null) { return; }

        NpyPath = npy.Path;

        RepetitionsMaximum = MotionDiffusionModel.GetRepetitionsCount(NpyPath) - 1;
    }

    [RelayCommand(CanExecute = nameof(CanSaveVmdExecute))]
    private async void SaveVmd()
    {
        var saveFile = await StorageHelper.PickSaveFileAsync(new KeyValuePair<string, IList<string>>("VMD File", new[] { ".vmd" }));
        if(saveFile is null) { return; }

        MotionDiffusionModel.ConvertToVmd(NpyPath, TargetRepetition, saveFile.Path);
    }

    private bool CanSaveVmdExecute() => !string.IsNullOrWhiteSpace(NpyPath);
}
