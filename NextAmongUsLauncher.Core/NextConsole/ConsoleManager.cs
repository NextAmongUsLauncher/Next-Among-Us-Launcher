using System.Text;
using Vanara.PInvoke;

namespace NextAmongUsLauncher.Core.NextConsole;

public class ConsoleManager
{
    private static HWND HWND;
    private static User32.WINDOWINFO _windowinfo;
    
    internal bool ConsoleIsPresence => HWND != IntPtr.Zero;
    internal bool ConsoleIsOpen { get; private set; }
    public TextWriter? ConsoleOut { get; private set; }

    private string Title;

    public ConsoleManager(string title)
    {
        Title = title;
    }
    
    public void CreateConsole()
    {
        Kernel32.AllocConsole();
        HWND = Kernel32.GetConsoleWindow();
        User32.GetWindowInfo(HWND, ref _windowinfo);
        Console.Title = Title;
        Console.OutputEncoding = Encoding.UTF8;
        ConsoleOut = Console.Out;
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