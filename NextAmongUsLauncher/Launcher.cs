using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using NextAmongUsLauncher.Core;
using NextAmongUsLauncher.Core.Base;
using NextAmongUsLauncher.Core.NextConsole;
using NextAmongUsLauncher.Core.Utils;

namespace NextAmongUsLauncher;

public sealed class Launcher
{
    /// <summary>
    ///     启动器版本号
    /// </summary>
    public static readonly Version LauncherVersion = new(1, 0, 0);

    /// <summary>
    ///     主窗口
    /// </summary>
    public readonly MainWindow MainWindow;

    public Launcher(MainWindow Window)
    {
        Assembly.GetExecutingAssembly().GetName().Version = LauncherVersion;
        Instance = this;
        MainWindow = Window;
        MainWindow.Activate();
        AllWindow.Add(MainWindow);
        Start();
    }

    /// <summary>
    ///     启动器实例
    /// </summary>
    public static Launcher Instance { get; private set; }

    /// <summary>
    ///     是否为开发模式
    /// </summary>
    public static bool IsDev { get; private set; }

    /// <summary>
    ///     管理启动器控制台
    /// </summary>
    public ConsoleManager ConsoleManager { get; private set; }

    /// <summary>
    ///     管理游戏
    /// </summary>
    public GameManager GameManager { get; private set; }

    /// <summary>
    ///     所有窗口
    /// </summary>
    public HashSet<Window> AllWindow { get; } = new();

    /// <summary>
    ///     所有页面
    /// </summary>
    public HashSet<Page> AllPage { get; private set; } = new();

    /// <summary>
    ///     启动器服务
    /// </summary>
    public NextService LauncherService { get; private set; }
    
    public List<Server> PublicServers;

    private void Start()
    {
#if DEBUG
        SetDev(true);
#endif

        LauncherService = NextService.GetInstance(true);
        ConsoleManager = new ConsoleManager("Next Among Us Launcher");
        GameManager = new GameManager();
        

        Logger.Initialize(IsDev, ConsoleManager);
        GameManager.Init();
        Task.Factory.StartNew(DownloadPublicServers);
    }

    public void Close()
    {
    }

    internal void SetDev(bool dev)
    {
        IsDev = dev;
    }
    
    private void DownloadPublicServers()
    {
        var Url = new Uri("http://server.aumod.wiki:5244/d/Update/serverList.json");

        string? Document;

        try
        {
            var httpClientHandler = new HttpClientHandler();
            httpClientHandler.ServerCertificateCustomValidationCallback =
                (message, cert, chain, sslPolicyErrors) => true;
            using var client = new HttpClient(httpClientHandler);
            var res = client.GetAsync(Url);
            Document = res.Result.Content.ReadAsStringAsync().Result;
            Console.WriteLine(Document);
        }
        catch (Exception e)
        {
            Log.Exception(e);
            return;
        }

        var document = JsonDocument.Parse(Document);
        PublicServers = document.RootElement.EnumerateArray().GetServerFormArray();
    }
}