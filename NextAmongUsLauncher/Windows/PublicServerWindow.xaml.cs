using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using NextAmongUsLauncher.Core.Base;
using NextAmongUsLauncher.Helper;
using NextAmongUsLauncher.Pages;

namespace NextAmongUsLauncher.Windows;

public partial class PublicServerWindow : Window
{
    private ObservableCollection<Server> Servers;

    public PublicServerWindow()
    {
        AppWindow.Title = "公共服务器导入";
        Instance.AllWindow.Add(this);
        InitializeComponent();
    }

    public void Set()
    {
        if (Instance.PublicServers.Count == 0) return;
        Servers = new ObservableCollection<Server>(Instance.PublicServers.Where(Find));
    }

    private bool Find(Server server)
    {
        return Page_Server.Servers.All(server2 => Find(server, server2!));
    }

    private bool Find(Server server, Server server2)
    {
        if (server.Name == server2.Name)
            return false;

        if (server.PingServer == server2.PingServer)
            return false;

        if (server.ServerInfos == server2.ServerInfos)
            return false;
        
        return true;
    }

    private void ListView_OnItemClick(object sender, ItemClickEventArgs e)
    {
        Page_Server.Servers.Add(e.ClickedItem as Server);
        AppWindow.Destroy();
    }

    public static void OpenWindow()
    {
        if (Instance.AllWindow.Any(n => n is PublicServerWindow))
        {
            var window = Instance.AllWindow.FirstOrDefault(n => n is PublicServerWindow) as PublicServerWindow;
            window!.Set();
            window.WindowTop();
        }

        var NewWindow = new PublicServerWindow();
        NewWindow.Set();
        NewWindow.Activate();
    }
}