namespace NextAmongUsLauncher.Core.NextConsole.Logs;

/// <summary>
///     控制台信息日志等级
/// </summary>
[Flags]
public enum LogLevel
{
    /// <summary>
    ///     默认
    /// </summary>
    None,

    /// <summary>
    ///     调试
    /// </summary>
    Debug,

    /// <summary>
    ///     信息
    /// </summary>
    Info,

    /// <summary>
    ///     警告
    /// </summary>
    Warning,

    /// <summary>
    ///     错误
    /// </summary>
    Error,

    /// <summary>
    ///     自动
    /// </summary>
    Auto,

    /// <summary>
    ///     所有
    /// </summary>
    All = Info | Debug | None | Warning | Error | Auto
}