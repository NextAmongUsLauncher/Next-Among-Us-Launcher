#nullable enable
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Windows.Storage.Pickers;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using NextAmongUsLauncher.Core.Base;
using NextAmongUsLauncher.Core.NextConsole;
using NextAmongUsLauncher.Core.Utils;
using NextAmongUsLauncher.Windows;
using WinRT.Interop;

namespace NextAmongUsLauncher.Pages;

public sealed partial class Page_Server : Page
{
    public static readonly string RegionConfigPath =
        $"{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}Low/Innersloth/Among Us/regionInfo.json";

    public Server? CurrentServer;
    
    public static ObservableCollection<Server?> Servers = new();

    public ObservableCollection<Server?> _Servers;
    
    private string RegionText = string.Empty;

    private static StreamWriter? _writer;

    public Page_Server()
    {
        InitializeComponent();
        
        
        if (RegionText == string.Empty)
        {
            var regionInfoString = AmongUsServerSerialization.Read(RegionConfigPath);
            var regionInfo = AmongUsServerSerialization.Deserialization(regionInfoString);
            Servers = new ObservableCollection<Server?>(regionInfo?.Regions!);
            Servers.CollectionChanged += OnServersChanged;
        }

        _Servers = Servers;
        Unloaded += OnUnloaded;
    }

    private void OnUnloaded(object sender, RoutedEventArgs routedEventArgs)
    {
        _writer ??= new StreamWriter(File.Open(RegionConfigPath, FileMode.OpenOrCreate, FileAccess.ReadWrite));
        _writer.Write(RegionText);
        _writer.Close();
    }

    private void OnServersChanged(object? sender, NotifyCollectionChangedEventArgs notifyCollectionChangedEventArgs)
    {
        _Servers = Servers;
        RegionText = AmongUsServerSerialization.Serialization(Servers.ToList());
    }

    private void ServersList_OnItemClick(object sender, ItemClickEventArgs e)
    {
        CurrentServer = e.ClickedItem as Server;
    }

    private void EditButton_OnClick(object sender, RoutedEventArgs e)
    {
        if (CurrentServer == null)
            return;

        ServerEditWindow.OpenWindow(CurrentServer);
    }

    private void OpenPublicServerWindow(object sender, RoutedEventArgs e)
    {
        PublicServerWindow.OpenWindow();
    }
    

    private void RemoveButton_Click(object sender, RoutedEventArgs e)
    {
        Servers.Remove(CurrentServer!);
    }

    private void AddButton_Click(object sender, RoutedEventArgs e)
    {
        new AddServerWindow().Activate();
    }

    private async void ImportButton_Click(object sender, RoutedEventArgs e)
    {
        var picker = new FileOpenPicker
        {
            ViewMode = PickerViewMode.Thumbnail,
            SuggestedStartLocation = PickerLocationId.PicturesLibrary
        };
        
        InitializeWithWindow.Initialize(picker, Instance.MainWindow.HWND);
        picker.FileTypeFilter.Add(".json");
        var file = await picker.PickSingleFileAsync();
        
        foreach (var server 
                 in 
                 JsonDocument.Parse(File.ReadAllTextAsync(file.Path).Result)
                     .RootElement.EnumerateArray().
                     GetServerFormArray().
                     FindAll(n => !Servers.Contains(n)))
        {
            Servers.Add(server);
        }
    }
}