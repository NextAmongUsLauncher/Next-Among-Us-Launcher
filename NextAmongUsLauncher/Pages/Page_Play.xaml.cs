using Microsoft.UI.Xaml.Controls;
using NextAmongUsLauncher.Core;

namespace NextAmongUsLauncher.Pages;


public sealed partial class Page_Play : Page
{
    public Page_Play()
    {
        InitializeComponent();

        var mod = ModManager.Get().GetCurrentMod();
        TitleText.Text = mod.Name;
        VersionText.Text = mod.Version;
    }
}