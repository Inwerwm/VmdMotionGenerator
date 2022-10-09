using Microsoft.UI.Xaml.Controls;

using VmdMotionGenerator.ViewModels;

namespace VmdMotionGenerator.Views;

public sealed partial class MainPage : Page
{
    public MainViewModel ViewModel
    {
        get;
    }

    public MainPage()
    {
        ViewModel = App.GetService<MainViewModel>();
        InitializeComponent();
    }
}
