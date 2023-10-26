using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace NextAmongUsLauncher.Pages;

/// <summary>
///     An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class Page_Setting : Page
{
    public Page_Setting()
    {
        InitializeComponent();
    }

    private void LangsComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
    }

    private void PickSteamFolderButton_Click(object sender, RoutedEventArgs e)
    {
    }

    private void PickEpicFolderButton_Click(object sender, RoutedEventArgs e)
    {
    }
}