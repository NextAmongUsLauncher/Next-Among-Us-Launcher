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
    private AmongUsServerSerialization _serialization;

    private Server CurrentServer;

    private ItemClickEventArgs _Item;
    
    public ServerEditWindow()
    {
        InitializeComponent();

        AppWindow.Title = "Server Edit";
        AppWindow.Resize(new SizeInt32(400, 400));
        AppWindow.Destroying += OnDestroy;
        Launcher.Instance.AllWindow.Add(this);
    }

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

    private void OnDestroy(AppWindow appWindow, object sender)
    {
        
    }

    public static ServerEditWindow OpenWindow(ItemClickEventArgs e, AmongUsServerSerialization serialization)
    {
        ServerEditWindow EditWindow;
        if (Launcher.Instance.AllWindow.Any(n => n is ServerEditWindow))
        {
            EditWindow = Launcher.Instance.AllWindow.First(n => n is ServerEditWindow) as ServerEditWindow;
            if (!EditWindow!.AppWindow.IsVisible)
            {
                EditWindow!.AppWindow.Show();
            }
        }
        else
        {
            EditWindow = new ServerEditWindow();
            EditWindow.Activate();
        }
        
        EditWindow.Set(e);
        EditWindow.Set(serialization);
        
        EditWindow.AppWindow.MoveInZOrderAtTop();

        return EditWindow;
    }
}