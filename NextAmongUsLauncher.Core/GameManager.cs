using Microsoft.Win32;

namespace NextAmongUsLauncher.Core;

public class GameManager
{
    /*/*public void StartInit()
    {
        Find();
    }*/

    private async void Find()
    {
        await FindFormDirectory();
    }

    private ValueTask<List<string>> FindFormDirectory()
    {
        return new ValueTask<List<string>>();
    }

    public static List<string> FindFormRegedit()
    {

        var registry =
#pragma warning disable CA1416
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
                ?.GetValueNames().Where(n => n.EndsWith("Among Us.exe")).ToList();
#pragma warning restore CA1416

        return registry ?? new List<string>();
    }
}