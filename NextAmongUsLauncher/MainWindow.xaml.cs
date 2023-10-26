using System;
using System.Collections.Generic;
using Windows.Graphics;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using NextAmongUsLauncher.Core;
using NextAmongUsLauncher.Pages;

namespace NextAmongUsLauncher;

public sealed partial class MainWindow : Window
{
    public new static MainWindow Current { get; private set; }
    public static (Type, object) CurrentPage { get; private set; }
    public ModManager ModManager;

    public MainWindow()
    {
        Current = this;
        InitializeComponent();

        SystemBackdrop = new DesktopAcrylicBackdrop();

        AppWindow.MoveAndResize(new RectInt32(335, 165, 1250, 750));

        ExtendsContentIntoTitleBar = true;
        SetTitleBar(AppTitleBar);
        
        ModManager = new ModManager();
    }

    private void Main_Navigation_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
    {
        var SelectedItem = (NavigationViewItem)args.SelectedItem;
        
        var pageType = SelectedItem.Tag switch
        {
            "Play" => typeof(Page_Play),
            "Version" => typeof(Page_Version),
            "About" => typeof(Page_About),
            "Setting" => typeof(Page_Setting),
            _ => null
        };
        
        CurrentPage = (pageType, SelectedItem.Tag);
        contentFrame.Navigate(pageType);
    }
}