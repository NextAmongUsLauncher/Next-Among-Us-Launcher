using System;
using System.Collections.Generic;
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

    private readonly HashSet<NavigationViewItem> _Pages = new();
    private readonly HashSet<Type> _PageTypes;
    

    public MainWindow()
    {
        Current = this;
        InitializeComponent();
        
        SystemBackdrop = new DesktopAcrylicBackdrop();

        AppWindow.MoveAndResize(new RectInt32(335, 165, 1250, 750));

        ExtendsContentIntoTitleBar = true;
        SetTitleBar(AppTitleBar);
        Main_NavigationView.MenuItems.ToList().ForEach(n => _Pages.Add(n as NavigationViewItem));
        Main_NavigationView.FooterMenuItems.ToList().ForEach(n => _Pages.Add(n as NavigationViewItem));
        _PageTypes =
            new HashSet<Type>(Assembly.GetExecutingAssembly().GetTypes().Where(n => n.BaseType == typeof(Page)));

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
        Navigate(_Pages.FirstOrDefault(n => ReferenceEquals(n.Tag, n.Name.Split("_")[1])), type);

    private void Navigate(NavigationViewItem item) =>
        Navigate(item, _PageTypes.FirstOrDefault(n => ReferenceEquals(n.Name.Split("_")[1], item.Tag)));

    private void Navigate(NavigationViewItem item, Type type)
    {
        Main_NavigationView.SelectedItem = item;
        
        CurrentPage = item;
        contentFrame.Navigate(type);
    }
}