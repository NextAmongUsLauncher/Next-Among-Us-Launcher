using System.Reflection;

namespace NextAmongUsLauncher.Core;

public sealed class Mod
{
    public string? Author;
    public string? Description;
    public bool IsDefault;
    public string? License;
    public object? ModBaseClass;
    public Assembly? ModInstance;
    public string? Name;
    public string? PUID;
    public string? URL;
    public string? Version;
}