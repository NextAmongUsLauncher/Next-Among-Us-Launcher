using System.Collections.ObjectModel;
using Microsoft.UI.Xaml;
using NextAmongUsLauncher.Core.Base;

namespace NextAmongUsLauncher.Windows;

public partial class PublicServerWindow : Window
{
    public ObservableCollection<Server> Servers = new();
    
    public PublicServerWindow(ObservableCollection<Server> servers)
    {
        Servers = servers;
        InitializeComponent();
    }
}