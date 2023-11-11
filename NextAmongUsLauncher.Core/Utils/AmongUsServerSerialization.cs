using System.Text.Json;
using NextAmongUsLauncher.Core.Base;

namespace NextAmongUsLauncher.Core.Utils;

public class AmongUsServerSerialization
{
    public AmongUsServerSerialization(string? serverConfigString)
    {
        ServerConfigString = serverConfigString;
    }
    
    public AmongUsServerSerialization() {}

    
    public int CurrentRegionIdx { get; private set; }
    
    
    public int ServerCount { get; private set; }


    public string? ServerConfigString { get; private set; }
    

    public List<Server>? AllServer { get; private set; }

    public void Serialization()
    {
        
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