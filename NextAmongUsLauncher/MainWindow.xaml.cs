using System;
using Windows.Graphics;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using NextAmongUsLauncher.Pages;

namespace NextAmongUsLauncher;

public sealed partial class MainWindow : Window
{
    public IntPtr hWnd;

    public MainWindow()
    {
        Current = this;
        InitializeComponent();

        SystemBackdrop = new DesktopAcrylicBackdrop();

        AppWindow.MoveAndResize(new RectInt32(335, 165, 1250, 750));

        ExtendsContentIntoTitleBar = true;
        SetTitleBar(AppTitleBar);
    }

    public new static MainWindow Current { get; private set; }

    private void GloabalNavigation_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
    {
        var pageType = ((NavigationViewItem)args.SelectedItem).Tag switch
        {
            "Play" => typeof(Page_Play),
            "Version" => typeof(Page_Version),
            "About" => typeof(Page_About),
            "Setting" => typeof(Page_Setting),
            _ => typeof(Page_Play)
        };

        _ = contentFrame.Navigate(pageType);
    }
}