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
using Windows.Storage.Pickers;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using NextAmongUsLauncher.Core.Base;
using NextAmongUsLauncher.Core.NextConsole;
using NextAmongUsLauncher.Core.Utils;
using NextAmongUsLauncher.Windows;

namespace NextAmongUsLauncher.Pages;

public sealed partial class Page_Server : Page
{
    public static readonly string RegionConfigPath =
        $"{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}Low/Innersloth/Among Us/regionInfo.json";

    public Server? CurrentServer;

    public List<Server>? PublicServers;
    
    public static ObservableCollection<Server> Servers = new();

    public ObservableCollection<Server> _Servers = new();

    public bool Downloaded = false;

    private string RegionText = string.Empty;


    public Page_Server()
    {
        InitializeComponent();
        
        
        if (ServerSerialization == null)
        {
            ServerSerialization = new AmongUsServerSerialization();
            using Stream stream = new FileStream(RegionConfigPath, FileMode.Open, FileAccess.Read);
            using TextReader reader = new StreamReader(stream);
            ServerSerialization.Deserialization(reader.ReadToEnd());
            Servers = new ObservableCollection<Server>(ServerSerialization.AllServer!);
            Servers.CollectionChanged += OnServersChanged;
        }

        _Servers = Servers;
        Unloaded += OnUnloaded;
    }

    public static AmongUsServerSerialization? ServerSerialization { get; private set; }

    private void OnUnloaded(object sender, RoutedEventArgs routedEventArgs)
    {
        using var file = File.Open(RegionConfigPath, FileMode.OpenOrCreate, FileAccess.Write);
        using var Writer = new StreamWriter(file, Encoding.UTF8);
        Writer.WriteAsync(RegionText);
    }

    private void OnServersChanged(object? sender, NotifyCollectionChangedEventArgs notifyCollectionChangedEventArgs)
    {
        _Servers = Servers;
        RegionText = ServerSerialization?.Serialization(Servers.ToList())!;
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

    private void OpenPublicServerWindow(object sender, RoutedEventArgs e)
    {
        DownloadPublicServers();
        PublicServerWindow.OpenWindow(this);
    }

    private void DownloadPublicServers(bool gitee = false)
    {
        if (PublicServers != null) return;
        if (Downloaded) return;
        
        var GiteeUrl = new Uri("https://gitee.com/bilibili_MC/Resources/raw/main/PublicServerList.json");
        var GithubUrl =
            new Uri("https://raw.githubusercontent.com/NextAmongUsLauncher/Resources/main/PublicServerList.json");

        var url = gitee ? GiteeUrl : GithubUrl;

        string Document;

        try
        {
            var httpClientHandler = new HttpClientHandler();
            httpClientHandler.ServerCertificateCustomValidationCallback =
                (message, cert, chain, sslPolicyErrors) => true;
            using var client = new HttpClient(httpClientHandler);
            var res = client.GetAsync(url);
            Document = res.Result.Content.ReadAsStringAsync().Result;
        }
        catch (Exception e)
        {
            Log.Exception(e);
            if (gitee)
                return;
            DownloadPublicServers(true);
            return;
        }

        var document = JsonDocument.Parse(Document);
        PublicServers = document.RootElement.EnumerateArray().GetServerFormArray();
        Downloaded = true;
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
        picker.FileTypeFilter.Add(".json");
        var file = await picker.PickSingleFileAsync();
        if (file == null)
            return;
        
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