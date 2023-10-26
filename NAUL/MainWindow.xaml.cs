using Microsoft.UI.Composition.SystemBackdrops;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using WinRT.Interop;
using Microsoft.UI;
using Microsoft.UI.Windowing;
using Windows.Graphics;
using Windows.Storage;
using System.Runtime.InteropServices;
using Windows.System;
using System.Security.AccessControl;

namespace NAUL;

public sealed partial class MainWindow : Window
{
    public IntPtr hWnd;
    private AppWindow appWindow;

    public MainWindow()
    {
        this.InitializeComponent();

        SystemBackdrop = new DesktopAcrylicBackdrop();

        hWnd = WindowNative.GetWindowHandle(this);
        WindowId id = Win32Interop.GetWindowIdFromWindow(hWnd);
        appWindow = AppWindow.GetFromWindowId(id);

        appWindow.MoveAndResize(new RectInt32(_X: 335, _Y: 165, _Width: 1250, _Height: 750));

        this.ExtendsContentIntoTitleBar = true;
        this.SetTitleBar(AppTitleBar);

    }

    private void GloabalNavigation_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
    {
        Type pageType = ((NavigationViewItem)args.SelectedItem).Tag switch
        {
            "Play" => typeof(Page_Play),
            "Version" => typeof(Page_Version),
            "About" => typeof(Page_About),
            "Setting" => typeof(Page_Setting),
            _ => typeof(Page_Play),
        };

        _ = contentFrame.Navigate(pageType);
    }
}
