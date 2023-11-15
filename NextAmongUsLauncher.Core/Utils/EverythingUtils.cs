using EverythingSharp;

namespace NextAmongUsLauncher.Core.Utils;

public class EverythingUtils
{
    private readonly EverythingSharp.Base? Everything = EverythingSharp.Base.Get();

    public static Everything? Everything64;
    
    public static Everything? Everything32;

    private static EverythingUtils? _instance;

    public static EverythingUtils Instance
    {
        get { return _instance ??= new EverythingUtils();　}
        set => _instance = value;
    }

    private const string URL =
        "https://raw.githubusercontent.com/Huier-Huang/EverythingSharp/master/EverythingSharp/EverythingSharp/DLL";

    public EverythingUtils()
    {
        if (Directory.Exists("./DLL"))
            Directory.CreateDirectory("./DLL");

        if (File.Exists("./DLL/Everything32.dll") && Everything is Everything everything32)
        {
            DownloadEverything("./DLL/", "Everything32.dll");
            Everything32 = everything32;
        }

        if (File.Exists("./DLL/Everything64.dll") && Everything is Everything everything64)
        {
            DownloadEverything("./DLL/", "Everything64.dll");
            Everything64 = everything64;
        }
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