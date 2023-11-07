using Vanara.PInvoke;

namespace NextAmongUsLauncher.Core.NextConsole;

public class ConsoleManager
{
    private static HWND HWND;
    private static User32.WINDOWINFO _windowinfo;
    
    internal bool ConsoleIsPresence => HWND != IntPtr.Zero;
    internal bool ConsoleIsOpen { get; private set; }
    internal TextWriter? ConsoleOut { get; private set; }

    private string Title;

    public void Init(string title = "Next Among Us Launcher")
    {
        
    }
    
    public void CreateConsole()
    {
        Kernel32.AllocConsole();
        HWND = Kernel32.GetConsoleWindow();
        User32.GetWindowInfo(HWND, ref _windowinfo);
        System.Console.Title = Title;
        ConsoleOut = System.Console.Out;
        ConsoleIsOpen = true;
    }

    public void CloseConsole()
    {
        Kernel32.FreeConsole();
        HWND = IntPtr.Zero;
        ConsoleIsOpen = false;
    }

    public void SetConsoleTitle(string title)
    {
        
    }
    
    public void Show()
    {
        if (!ConsoleIsPresence) return;
        
        User32.ShowWindow(HWND, ShowWindowCommand.SW_SHOWNORMAL);
        ConsoleIsOpen = true;
    }


    public void Hide()
    {
        if (!ConsoleIsPresence) return;
        
        User32.ShowWindow(HWND, ShowWindowCommand.SW_HIDE);
        ConsoleIsOpen = false;
    }
}