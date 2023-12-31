using EverythingSharp;

namespace NextAmongUsLauncher.Core.Utils;

public class EverythingUtils
{
    private const string URL =
        "https://raw.githubusercontent.com/Huier-Huang/EverythingSharp/master/EverythingSharp/EverythingSharp/DLL";

    public static Everything? Everything64;

    public static Everything? Everything32;

    private static EverythingUtils? _instance;
    public readonly EverythingSharp.Base? Everything = EverythingSharp.Base.Get();

    private EverythingUtils()
    {
        if (Everything is Everything everything32) Everything32 = everything32;

        if (Everything is Everything everything64) Everything64 = everything64;
    }

    public static EverythingUtils Instance
    {
        get { return _instance ??= new EverythingUtils();　}
        set => _instance = value;
    }

    private void DownloadEverything(string path, string name)
    {
        var url = $"{URL}/{name}";
        var FilePath = $"{path}/{name}";
        using Stream stream = File.Create(FilePath);
        using var client = new HttpClient();

        var EverythingStream = client.GetStreamAsync(new Uri(url)).Result;
        EverythingStream.CopyToAsync(stream);
    }
}