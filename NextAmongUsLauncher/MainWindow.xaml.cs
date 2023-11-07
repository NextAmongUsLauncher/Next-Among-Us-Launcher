using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
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
    public static NavigationViewItem CurrentPage { get; private set; }

    private readonly Dictionary<NavigationViewItem, Type> _pages;
    

    public MainWindow()
    {
        Current = this;
        InitializeComponent();
        
        SystemBackdrop = new DesktopAcrylicBackdrop();

        AppWindow.MoveAndResize(new RectInt32(335, 165, 1250, 750));

        ExtendsContentIntoTitleBar = true;
        SetTitleBar(AppTitleBar);

        _pages = new Dictionary<NavigationViewItem, Type>
        {
            { PlayerPage, typeof(Page_Play) },
            { AboutPage, typeof(Page_About) },
            { SettingPage, typeof(Page_Setting) },
            { VersionPage, typeof(Page_Version) },
            { ServerPage, typeof(Page_Server)}
        };

        if (CurrentPage == null)
        {
            Navigate(PlayerPage);
        }
        
    }
    

    private void Main_Navigation_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
    {
        var SelectedItem = (NavigationViewItem)args.SelectedItem;
        
        Navigate(SelectedItem);
    }

    private void Navigate(Type type)=>
        Navigate(_pages.FirstOrDefault(n => n.Value == type).Key, type);

    private void Navigate(NavigationViewItem item) =>
        Navigate(item, _pages[item]);

    private void Navigate(NavigationViewItem item, Type type)
    {
        if (item == null || type == null) return;
        Main_NavigationView.SelectedItem = item;
        
        CurrentPage = item;
        contentFrame.Navigate(type);
    }
}