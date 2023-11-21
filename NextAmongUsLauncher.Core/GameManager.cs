using System.Runtime.InteropServices;
using Microsoft.Win32;
using NextAmongUsLauncher.Core.Base;
using NextAmongUsLauncher.Core.Utils;

namespace NextAmongUsLauncher.Core;

public class GameManager
{
    /// <summary>
    /// Among Us EXE 名称
    /// </summary>
    public const string AmongUsExeName = "Among Us.exe";
    
    /// <summary>
    /// 存放寻找的Among Us平台列表
    /// </summary>
    public GamePathList GamePathList { get; private set; } = new();

    public void Init()
    {
        Find();
        
    }
    
    public async void Find()
    {
        GamePathList.Add(await FindFormDirectoryAsync());
        GamePathList.Add(await FindFormRegeditAsync());
    }
    
    /// <summary>
    /// 使用Everything寻找Among Us
    /// </summary>
    /// <returns>Exe路径</returns>
    public async ValueTask<List<string>> FindFormDirectoryAsync()
    {
        var list = (EverythingUtils.Instance.Everything?.Search(AmongUsExeName).Select(VarPath => VarPath.FullPath) ?? Array.Empty<string>()).ToList();
        return await ValueTask.FromResult(list);
    }
    
    /// <summary>
    /// 从注册表寻找Among Us
    /// </summary>
    /// <returns>Exe路径</returns>
    public async ValueTask<List<string>> FindFormRegeditAsync()
    {
        var list = new List<string>();
        
        if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) return await ValueTask.FromResult(list);
        
        var registry =
            Registry.CurrentUser.
                OpenSubKey("Software")
                ?.
                OpenSubKey("Microsoft")
                ?.
                OpenSubKey("Windows")
                ?.
                OpenSubKey("CurrentVersion")
                ?.
                OpenSubKey("Explorer")
                ?.
                OpenSubKey("FeatureUsage")
                ?.
                OpenSubKey("AppSwitched")
                ?.GetValueNames().Where(n => n.EndsWith(AmongUsExeName)).ToList();

        registry ??= list;
        
        return await ValueTask.FromResult(registry);
    }
}

public class GamePathList : List<Platform>, ICollection<Platform>
{
    bool ICollection<Platform>.IsReadOnly => false;
    
    public void Add(string Path)
    {
        if (Exists(n => n.GameExePath == Path)) return;
        var platform = Platform.GetPlatform(Path);
        if (platform == null) return;
        Add(platform);
    }

    public void Add(IEnumerable<string> list)
    {
        foreach (var path in list)
        {
            Add(path);
        }
    }
}