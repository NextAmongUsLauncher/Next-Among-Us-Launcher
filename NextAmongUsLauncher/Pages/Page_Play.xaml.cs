using System.Collections.ObjectModel;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using NextAmongUsLauncher.Core;

namespace NextAmongUsLauncher.Pages;


public sealed partial class Page_Play : Page
{
    private ObservableCollection<VersionItem> versions = new ();
    
    public Page_Play()
    {
        InitializeComponent();

        var mod = ModManager.Get().GetCurrentMod();
        TitleText.Text = mod.Name;
        VersionText.Text = mod.Version;
        TitleText.Visibility = VersionText.Visibility = mod.IsDefault ? Visibility.Collapsed : Visibility.Visible;
    }
}