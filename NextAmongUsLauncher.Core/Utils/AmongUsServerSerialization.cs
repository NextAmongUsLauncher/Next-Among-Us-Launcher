using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Unicode;
using NextAmongUsLauncher.Core.Base;
using NextAmongUsLauncher.Core.NextConsole;

namespace NextAmongUsLauncher.Core.Utils;


public class AmongUsServerSerialization
{
    public AmongUsServerSerialization(string? serverConfigString)
    {
        ServerConfigString = serverConfigString;
    }
    
    public AmongUsServerSerialization() {}

    
    [JsonPropertyOrder(0)]
    public int CurrentRegionIdx { get; private set; }
    
    [JsonIgnore]
    public int ServerCount { get; private set; }
    
    [JsonIgnore]
    public string? ServerConfigString { get; private set; }
    
    [JsonPropertyName("Regions"), JsonPropertyOrder(1)]
    public List<Server>? AllServer { get; private set; }

    public string Serialization()
    {
        var String = JsonSerializer.Serialize(this, new JsonSerializerOptions
        {
            Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.CjkUnifiedIdeographs)
        });
        return String;
    }

    public void Deserialization(string? serverConfigString = null)
    {
        if (serverConfigString != null)
        {
            ServerConfigString = serverConfigString;
        }

        if (ServerConfigString == null)
        {
            return;
        }
        
        DeserializationDocument(JsonDocument.Parse(ServerConfigString));
    }

    internal AmongUsServerSerialization DeserializationDocument(JsonDocument document)
    {
        var root = document.RootElement;
        var regions = root.GetProperty("Regions").EnumerateArray();
        
        CurrentRegionIdx = root.GetProperty("CurrentRegionIdx").GetInt32();
        ServerCount = regions.Count();
        AllServer = new List<Server>(ServerCount);
            
        foreach (var Server in from region in regions let servers = region.GetProperty("Servers").EnumerateArray() let infos = servers.Select(ServerInfo => new Server.ServerInfo(ServerInfo.GetProperty("Name").GetString(), ServerInfo.GetProperty("Ip").GetString(), ServerInfo.GetProperty("Port").GetUInt16(), ServerInfo.GetProperty("UseDtls").GetBoolean(), ServerInfo.GetProperty("Players").GetInt32(), ServerInfo.GetProperty("ConnectionFailures").GetInt32())).ToList() select new Server(
                 
                     region.GetProperty("$type").GetString()!,
                     region.GetProperty("Name").GetString()!,
                     region.GetProperty("PingServer").GetString()!,
                     region.GetProperty("TargetServer").GetString(),
                     region.GetProperty("TranslateName").GetInt32(), 
                     infos
                 ))
        {
            AllServer.Add(Server);
        }
        
        return this;
    }
}