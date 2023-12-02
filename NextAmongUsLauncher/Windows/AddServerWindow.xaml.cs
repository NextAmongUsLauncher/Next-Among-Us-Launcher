using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using NextAmongUsLauncher.Core.Base;
using NextAmongUsLauncher.Pages;

namespace NextAmongUsLauncher.Windows;

public partial class AddServerWindow : Window
{
    private string ServerName;
    
    private string ServerIp;

    private ushort Port;
    
    
    public AddServerWindow()
    {
        InitializeComponent();
    }

    private void AddButton_OnClick(object sender, RoutedEventArgs e)
    {
        Page_Server.Servers.Add
        (
            new Server
            (
                "StaticHttpRegionInfo, Assembly-CSharp",
                ServerName,
                ServerIp,
                null,
                1003,
                new List<Server.ServerInfo>()
                {
                    new(
                            ServerName, 
                            ((RadioButton)ConnectionProtocolButtons.SelectedItem).Tag.ToString() + "://" + ServerIp,
                            Port,
                            false,
                            0,
                            0
                        )
                }
            )
        );
        
        Close();
    }

    private void ServerName_TextBox_OnTextChanged(object sender, TextChangedEventArgs e)
    {
        ServerName = ServerName_TextBox.Text;
    }

    private void ServerIp_TextBox_OnTextChanged(object sender, TextChangedEventArgs e)
    {
        ServerIp = ServerIp_TextBox.Text;
    }

    private void ServerPort_TextBox_OnTextChanged(object sender, TextChangedEventArgs e)
    {
        Port = ushort.Parse(ServerPort_TextBox.Text);
    }
}