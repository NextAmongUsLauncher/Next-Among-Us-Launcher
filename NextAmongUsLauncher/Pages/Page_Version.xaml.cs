using System.Collections.ObjectModel;
using System.IO;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace NextAmongUsLauncher.Pages;

public sealed partial class Page_Version : Page
{
    public ObservableCollection<VersionItem> versions = new();

    public Page_Version()
    {
        InitializeComponent();
        versions = SetData();
    }

    private ObservableCollection<VersionItem> SetData()
    {
        var data = new ObservableCollection<VersionItem>
        {
            new("TONX", "Beta", "2023.10.24s", VersionPlatform.Steam,
                "F:/Game/Platform/Steam/steamapps/common/Among Us/"),
            new("TOHE", "2.3.6", "2023.10.24e", VersionPlatform.Epic, ""),
            new("TOH", "5.1.1", "2023.10.24e", VersionPlatform.Epic, ""),
            new("Extream Roles", "9.0.0.0", "2023.10.24s", VersionPlatform.Steam, "")
        };
        return data;
    }

    private void VersionsList_ItemClick(object sender, ItemClickEventArgs e)
    {
    }

    private void MenuFlyoutItem_Click(object sender, RoutedEventArgs e)
    {
    }

    private void OepnLocation_Click(object sender, RoutedEventArgs e)
    {
    }
}

public class VersionItem
{
    public VersionItem(string name, string modVersion, string gameVersion, VersionPlatform platform,
        string folderLocation, string fontGlyph = "\uE7FC")
    {
        var broken = !Directory.Exists(folderLocation);
        Name = (broken ? "(ÎÞÐ§) " : string.Empty) + name;
        ModVersion = modVersion;
        GameVersion = gameVersion;
        Platform = platform;
        FolderLocation = folderLocation;
        FontGlyph = broken ? "\uE729" : fontGlyph;
    }

    public string Name { get; set; }
    public string ModVersion { get; set; }
    public string GameVersion { get; set; }
    public VersionPlatform Platform { get; set; }
    public string FolderLocation { get; set; }
    public string FontGlyph { get; set; }
}

public enum VersionPlatform
{
    Steam,
    Epic
}