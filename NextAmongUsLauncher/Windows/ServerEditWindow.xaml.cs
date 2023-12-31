using System.Linq;
using Windows.Graphics;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using NextAmongUsLauncher.Core.Base;
using NextAmongUsLauncher.Core.Utils;

namespace NextAmongUsLauncher.Windows;

public partial class ServerEditWindow : Window
{
    private ItemClickEventArgs _Item;
    private AmongUsServerSerialization _serialization;

    private Server CurrentServer;

    public ServerEditWindow()
    {
        InitializeComponent();

        AppWindow.Title = "Server Edit";
        AppWindow.Resize(new SizeInt32(400, 400));
        AppWindow.Destroying += OnDestroy;
        Instance.AllWindow.Add(this);
        Current = this;
    }

    public new static ServerEditWindow Current { get; private set; }

    public void Set(AmongUsServerSerialization serialization)
    {
        _serialization = serialization;
    }

    public void Set(ItemClickEventArgs e)
    {
        _Item = e;
        CurrentServer = _Item.ClickedItem as Server;
        ServerName.Text = CurrentServer!.Name;
    }

    public void Set(Server server)
    {
        CurrentServer = server;
        ServerName.Text = CurrentServer.Name;
    }

    private void OnDestroy(AppWindow appWindow, object sender)
    {
        Instance.AllWindow.Remove(this);
        Current = null;
    }

    public static ServerEditWindow OpenWindow(Server server = null, AmongUsServerSerialization serialization = null)
    {
        if (Current != null)
        {
            Current.Set(server);
            Current.Set(serialization);

            if (Current.AppWindow.IsVisible) return Current;
            Current.AppWindow.Show();
            Current.AppWindow.MoveInZOrderAtTop();

            return Current;
        }

        ServerEditWindow EditWindow;
        if (Instance.AllWindow.Any(n => n is ServerEditWindow))
        {
            EditWindow = Instance.AllWindow.First(n => n is ServerEditWindow) as ServerEditWindow;
            if (!EditWindow!.AppWindow.IsVisible) EditWindow!.AppWindow.Show();
        }
        else
        {
            EditWindow = new ServerEditWindow();
            EditWindow.Activate();
        }

        EditWindow.Set(server);
        EditWindow.Set(serialization);
        EditWindow.AppWindow.MoveInZOrderAtTop();

        return EditWindow;
    }
}