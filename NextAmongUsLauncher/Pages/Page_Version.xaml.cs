using System.Collections.ObjectModel;
using System.IO;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace NextAmongUsLauncher.Pages;

public sealed partial class Page_Version : Page
{
    private ObservableCollection<VersionItem> versions = new();
    public Page_Version()
    {
        this.InitializeComponent();
        versions = SetData();
    }
    private ObservableCollection<VersionItem> SetData()
    {
        var data = new ObservableCollection<VersionItem>
        {
            new VersionItem("TONX", "Beta", "2023.10.24s", VersionPlatform.Steam, "F:/Game/Platform/Steam/steamapps/common/Among Us/"),
            new VersionItem("TOHE", "2.3.6", "2023.10.24e", VersionPlatform.Epic, ""),
            new VersionItem("TOH", "5.1.1", "2023.10.24e", VersionPlatform.Epic, ""),
            new VersionItem("Extream Roles", "9.0.0.0", "2023.10.24s", VersionPlatform.Steam, ""),
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
    public string Name { get; set; }
    public string ModVersion { get; set; }
    public string GameVersion { get; set; }
    public VersionPlatform Platform { get; set; }
    public string FolderLocation { get; set; }
    public string FontGlyph { get; set; }

    public VersionItem(string name, string modVersion, string gameVersion, VersionPlatform platform, string folderLocation, string fontGlyph = "\uE7FC")
    {
        var broken = !Directory.Exists(folderLocation);
        this.Name = (broken ? "(ÎÞÐ§) " : string.Empty) + name;
        this.ModVersion = modVersion;
        this.GameVersion = gameVersion;
        this.Platform = platform;
        this.FolderLocation = folderLocation;
        this.FontGlyph = broken ? "\uE729" : fontGlyph;
    }
}

public enum VersionPlatform
{
    Steam,
    Epic
}