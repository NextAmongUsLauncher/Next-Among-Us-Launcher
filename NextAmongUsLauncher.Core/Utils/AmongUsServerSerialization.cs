using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Unicode;
using NextAmongUsLauncher.Core.Base;

namespace NextAmongUsLauncher.Core.Utils;

public static class AmongUsServerSerialization
{
    public static readonly JsonSerializerOptions _SerializerOptions = new()
    {
        Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.CjkUnifiedIdeographs)
    };

    public static int CurrentRegionIdx;

    public static string Serialization(List<Server?> allServers)
    {
        var ser = new RegionInfo
        {
            Regions = allServers,
            CurrentRegionIdx = CurrentRegionIdx
        };
        return JsonSerializer.Serialize(ser, _SerializerOptions);
    }

    public static RegionInfo? Deserialization(string serverConfigString)
    {
        return JsonSerializer.Deserialize<RegionInfo>(serverConfigString);
    }

    public static string Read(this Stream stream)
    {
        using TextReader reader = new StreamReader(stream);
        return reader.ReadToEnd();
    }

    public static string Read(string path)
    {
        return !File.Exists(path) ? string.Empty : Read(File.OpenRead(path));
    }

    public static void Write(this Stream stream, RegionInfo regionInfo)
    {
        using TextWriter writer = new StreamWriter(stream);
        writer.Write(regionInfo.ToString());
    }

    public static void Write(string path, RegionInfo regionInfo)
    {
        using var file = File.Open(path, FileMode.OpenOrCreate, FileAccess.Write);
        Write(file, regionInfo);
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