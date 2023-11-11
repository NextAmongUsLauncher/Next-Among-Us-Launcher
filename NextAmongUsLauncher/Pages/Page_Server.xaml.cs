using System;
using System.Collections.ObjectModel;
using System.IO;
using Microsoft.UI.Xaml.Controls;
using NextAmongUsLauncher.Core.Base;
using NextAmongUsLauncher.Core.Utils;

namespace NextAmongUsLauncher.Pages;

public sealed partial class Page_Server : Page
{
    public ObservableCollection<Server> Servers = new();

    public AmongUsServerSerialization ServerSerialization;
    public Page_Server()
    {
        InitializeComponent();
        
        ServerSerialization = new AmongUsServerSerialization();
        using Stream stream = new FileStream($"{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}Low/Innersloth/Among Us/regionInfo.json", FileMode.Open, FileAccess.Read);
        TextReader reader = new StreamReader(stream);
        ServerSerialization.Deserialization(reader.ReadToEnd());
        ServerSerialization.AllServer!.ForEach(n => Servers.Add(n));
    }

    private void ServersList_OnItemClick(object sender, ItemClickEventArgs e)
    {
    }
}