#nullable enable
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using NextAmongUsLauncher.Core.Base;
using NextAmongUsLauncher.Core.Utils;
using NextAmongUsLauncher.Windows;

namespace NextAmongUsLauncher.Pages;

public sealed partial class Page_Server : Page
{
    public ObservableCollection<Server> Servers = new();

    public AmongUsServerSerialization ServerSerialization { get; private set; }

    public static readonly string RegionConfigPath =
        $"{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}Low/Innersloth/Among Us/regionInfo.json";

    public Server? CurrentServer;
    
    public Page_Server()
    {
        InitializeComponent();
        
        ServerSerialization = new AmongUsServerSerialization();
        using Stream stream = new FileStream(RegionConfigPath, FileMode.Open, FileAccess.Read);
        TextReader reader = new StreamReader(stream, Encoding.UTF8);
        ServerSerialization.Deserialization(reader.ReadToEnd());
        Servers = new ObservableCollection<Server>(ServerSerialization.AllServer!);
        Launcher.Instance.MainWindow.AppWindow.Changed += OnWindowChanged;
    }

    private void OnWindowChanged(AppWindow sender, AppWindowChangedEventArgs args)
    {
        ServersList.Height = sender.Size.Height - 100;
    }

    private void ServersList_OnItemClick(object sender, ItemClickEventArgs e)
    {
        CurrentServer = e.ClickedItem as Server;
    }

    private void EditButton_OnClick(object sender, RoutedEventArgs e)
    {
        if (CurrentServer == null)
            return;

        ServerEditWindow.OpenWindow(CurrentServer, ServerSerialization);
    }
}