#nullable enable
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Text.Json;
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
    public ObservableCollection<Server> Servers = new();


    public Page_Server()
    {
        InitializeComponent();

        ServerSerialization = new AmongUsServerSerialization();
        using Stream stream = new FileStream(RegionConfigPath, FileMode.Open, FileAccess.Read);
        TextReader reader = new StreamReader(stream, Encoding.UTF8);
        ServerSerialization.Deserialization(reader.ReadToEnd());
        Servers = new ObservableCollection<Server>(ServerSerialization.AllServer!);
        Instance.MainWindow.AppWindow.Changed += OnWindowChanged;
    }

    public AmongUsServerSerialization ServerSerialization { get; }

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

    private void OpenPublicServerWindow(object sender, RoutedEventArgs e)
    {
        DownloadPublicServers();
        PublicServerWindow.OpenWindow(this);
    }

    private void DownloadPublicServers(bool gitee = false)
    {
        if (PublicServers != null) return;
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
            DownloadPublicServers(true);
            Log.Exception(e);
            return;
        }

        var document = JsonDocument.Parse(Document);
        PublicServers = document.RootElement.EnumerateArray().GetServerFormArray();
    }

    private void RemoveButton_Click(object sender, RoutedEventArgs e)
    {
        Servers.Remove(CurrentServer!);
    }

    private void AddButton_Click(object sender, RoutedEventArgs e)
    {
    }

    private void ImportButton_Click(object sender, RoutedEventArgs e)
    {
    }
}