using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using NextAmongUsLauncher.Core.Base;
using NextAmongUsLauncher.Helper;
using NextAmongUsLauncher.Pages;

namespace NextAmongUsLauncher.Windows;
/*
 * 啊啊啊啊啊
 * 啊啊啊啊
 * 写出了屎山代码
 * 还有一堆奇奇怪怪的bug
 */
public partial class PublicServerWindow : Window
{
    public Page_Server _pageServer;

    public Server _server;
    public ObservableCollection<Server> Servers;

    public PublicServerWindow()
    {
        AppWindow.Title = "公共服务器导入";
        Instance.AllWindow.Add(this);
        AppWindow.Closing += (sender, args) => Instance.AllWindow.Remove(this);
        AppWindow.Destroying += (sender, args) => Instance.AllWindow.Remove(this);
        InitializeComponent();
    }

    public PublicServerWindow(Page_Server pageServer) : this()
    {
        Set(pageServer);
    }

    public void Set(Page_Server pageServer)
    {
        _pageServer = pageServer;
        Servers = new ObservableCollection<Server>(pageServer.PublicServers!.Where(Find));
    }

    private bool Find(Server server)
    {
        return Page_Server.Servers.All(server2 => Find(server, server2));
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

    public static PublicServerWindow OpenWindow(Page_Server pageServer)
    {
        if (Instance.AllWindow.Any(n => n is PublicServerWindow))
        {
            var window = Instance.AllWindow.FirstOrDefault(n => n is PublicServerWindow) as PublicServerWindow;
            window?.Set(pageServer);
            window.WindowTop();
            return window;
        }

        var NewWindow = new PublicServerWindow(pageServer);
        NewWindow.Activate();
        return NewWindow;
    }
}