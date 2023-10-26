using System;
using System.Collections.Generic;
using System.Linq;
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
    public ModManager ModManager;

    private readonly Dictionary<Type, NavigationViewItem> AllPageAndTag;

    public MainWindow()
    {
        Current = this;
        InitializeComponent();

        AllPageAndTag = new Dictionary<Type, NavigationViewItem>
        { 
            { typeof(Page_Play), PlayerPage},
            { typeof(Page_Version), VersionPage},
            { typeof(Page_About), AboutPage},
            { typeof(Page_Setting), SettingPage}
        };
        
        SystemBackdrop = new DesktopAcrylicBackdrop();

        AppWindow.MoveAndResize(new RectInt32(335, 165, 1250, 750));

        ExtendsContentIntoTitleBar = true;
        SetTitleBar(AppTitleBar);

        if (CurrentPage == null)
        {
            Navigate(PlayerPage);
        }
    }

    internal void InitializeCore()
    {
        ModManager = ModManager.Get();
    }

    private void Main_Navigation_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
    {
        var SelectedItem = (NavigationViewItem)args.SelectedItem;
        
        Navigate(SelectedItem);
    }

    private void Navigate(Type type)
    {
        var item = AllPageAndTag[type];
        
        if ((NavigationViewItem)Main_NavigationView.SelectedItem != item)
        {
            Main_NavigationView.SelectedItem = item;
        }
        
        CurrentPage = item;
        contentFrame.Navigate(type);
    }

    private void Navigate(NavigationViewItem item)
    {
        var type = AllPageAndTag.FirstOrDefault(n => n.Value == item).Key;
        
        if ((NavigationViewItem)Main_NavigationView.SelectedItem != item)
        {
            Main_NavigationView.SelectedItem = item;
        }
        
        CurrentPage = item;
        contentFrame.Navigate(type);
    }
}