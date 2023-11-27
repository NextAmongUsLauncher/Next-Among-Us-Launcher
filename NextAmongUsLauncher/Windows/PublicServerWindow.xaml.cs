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
    public ObservableCollection<Server> Servers;

    public Page_Server _pageServer;

    public Server _server;

    public PublicServerWindow()
    {
        AppWindow.Title = "公共服务器导入";
        Instance.AllWindow.Add(this);
        InitializeComponent();
    }

    public PublicServerWindow(Page_Server pageServer) : this() => Set(pageServer);

    public void Set(Page_Server pageServer)
    {
        Servers = new ObservableCollection<Server>(pageServer.PublicServers!.Where(n => !pageServer.Servers.Contains(n)));
        _pageServer = pageServer;
    }

    private void ListView_OnItemClick(object sender, ItemClickEventArgs e)
    {
        _pageServer.Servers.Add(e.ClickedItem as Server);
        Close();
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