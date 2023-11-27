using Microsoft.UI.Xaml;

namespace NextAmongUsLauncher;


public partial class App : Application
{
    public App()
    {
        InitializeComponent();
    }

    public static Launcher CurrentLauncher { get; private set; }


    protected override void OnLaunched(LaunchActivatedEventArgs args)
    {
        var window = new MainWindow();
        CurrentLauncher = new Launcher(window);
    }
}