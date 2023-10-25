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

namespace NAUL;

public sealed partial class MainWindow : Window
{
    private IntPtr hwnd;
    private AppWindow appWindow;

    public MainWindow()
    {
        this.InitializeComponent();

        SystemBackdrop = new DesktopAcrylicBackdrop();

        hwnd = WindowNative.GetWindowHandle(this);
        WindowId id = Win32Interop.GetWindowIdFromWindow(hwnd);
        appWindow = AppWindow.GetFromWindowId(id);

        appWindow.MoveAndResize(new RectInt32(_X: 335, _Y: 165, _Width: 1250, _Height: 750));

        this.ExtendsContentIntoTitleBar = true;
        this.SetTitleBar(AppTitleBar);

    }

    private void GloabalNavigation_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
    {
        Type pageType = args.IsSettingsSelected ? typeof(Page_Setting) : ((NavigationViewItem)args.SelectedItem).Tag switch
        {
            "Play" => typeof(Page_Play),
            "Version" => typeof(Page_Version),
            _ => typeof(Page_Play),
        };

        _ = contentFrame.Navigate(pageType);
    }
}
