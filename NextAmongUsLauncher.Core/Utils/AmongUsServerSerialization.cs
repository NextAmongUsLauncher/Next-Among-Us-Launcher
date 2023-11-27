using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Unicode;
using NextAmongUsLauncher.Core.Base;

namespace NextAmongUsLauncher.Core.Utils;

public class AmongUsServerSerialization
{
    [JsonIgnore] public readonly JsonSerializerOptions _SerializerOptions = new()
    {
        Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.CjkUnifiedIdeographs)
    };

    public AmongUsServerSerialization(string? serverConfigString)
    {
        ServerConfigString = serverConfigString;
    }

    public AmongUsServerSerialization()
    {
    }


    [JsonPropertyOrder(0)] public int CurrentRegionIdx { get; private set; }


    [JsonIgnore] public int ServerCount { get; private set; }


    [JsonIgnore] public string? ServerConfigString { get; private set; }


    [JsonPropertyName("Regions")]
    [JsonPropertyOrder(1)]
    public List<Server>? AllServer { get; private set; }


    public string Serialization(List<Server> allServers)
    {
        var ser = new AmongUsServerSerialization();
        ser.AllServer = allServers;
        ser.CurrentRegionIdx = CurrentRegionIdx;
        return JsonSerializer.Serialize(ser, _SerializerOptions);
    }

    public void Deserialization(string? serverConfigString = null)
    {
        if (serverConfigString != null)
            ServerConfigString = serverConfigString;

        if (ServerConfigString == null)
            return;

        DeserializationDocument(JsonDocument.Parse(ServerConfigString));
    }

    internal AmongUsServerSerialization DeserializationDocument(JsonDocument document)
    {
        var root = document.RootElement;
        var regions = root.GetProperty("Regions").EnumerateArray();

        CurrentRegionIdx = root.GetProperty("CurrentRegionIdx").GetInt32();
        ServerCount = regions.Count();
        AllServer = new List<Server>(ServerCount);

        AllServer.AddRange(regions.GetServerFormArray());

        return this;
    }

    public void Read(Stream stream)
    {
        using TextReader reader = new StreamReader(stream);
        ServerConfigString = reader.ReadToEnd();
    }

    public void Read(string path)
    {
        if (!File.Exists(path))
            return;

        Read(File.OpenRead(path));
    }

    public void Write(Stream stream)
    {
        using TextWriter writer = new StreamWriter(stream);
        writer.Write(ServerConfigString);
    }

    public void Write(string path)
    {
        using var file = File.Open(path, FileMode.OpenOrCreate, FileAccess.Write);
        Write(file);
    }
}

public static class FastServerSerialization
{
    public static List<Server> GetServerFormArray(this IEnumerable<JsonElement> enumerator)
    {
        var AllServer = new List<Server>();
        AllServer.AddRange(
            from
                region in enumerator
            let
                servers =
                region.GetProperty("Servers").EnumerateArray()
            let infos =
                servers.Select(ServerInfo => new
                    Server.ServerInfo(
                        ServerInfo.GetProperty("Name").GetString(),
                        ServerInfo.GetProperty("Ip").GetString(),
                        ServerInfo.GetProperty("Port").GetUInt16(),
                        ServerInfo.GetProperty("UseDtls").GetBoolean(),
                        ServerInfo.GetProperty("Players").GetInt32(),
                        ServerInfo.GetProperty("ConnectionFailures").GetInt32())
                ).ToList()
            select new Server(
                region.GetProperty("$type").GetString()!,
                region.GetProperty("Name").GetString()!,
                region.GetProperty("PingServer").GetString()!,
                Get(region, "TargetServer"),
                region.GetProperty("TranslateName").GetInt32(), infos)
        );

        return AllServer;

        string? Get(JsonElement element, string name)
        {
            return element.TryGetProperty(name, out element) ? element.GetString() : null;
        }
    }
}