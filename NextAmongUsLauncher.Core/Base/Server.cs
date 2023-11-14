using System.Text.Json.Serialization;

namespace NextAmongUsLauncher.Core.Base;

public sealed class Server
{
    public Server(string type, string name, string pingServer, string? targetServer, int translateName, List<ServerInfo> serverInfos)
    {
        Type = type;
        Name = name;
        PingServer = pingServer;
        TargetServer = targetServer;
        TranslateName = translateName;
        ServerInfos = serverInfos;
    }
    
    [JsonPropertyName("$type"), JsonPropertyOrder(0)]
    public string Type { get; set; }
    
    [JsonPropertyOrder(1)]
    public string Name { get; set; }
    
    [JsonPropertyOrder(2)]
    public string PingServer { get; set; }
    
    [JsonPropertyName("Servers"), JsonPropertyOrder(3)]
    public List<ServerInfo> ServerInfos { get; set; }
    
    [JsonPropertyOrder(4)]
    public string? TargetServer { get; set; }
    
    [JsonPropertyOrder(4)]
    public int TranslateName { get; set; }
    

    public sealed record ServerInfo(
        [property: JsonPropertyOrder(0)]string Name, 
        [property: JsonPropertyOrder(1)]string Ip, 
        [property: JsonPropertyOrder(2)]ushort Port, 
        [property: JsonPropertyOrder(3)]bool UseDtls, 
        [property: JsonPropertyOrder(4)]int Players, 
        [property: JsonPropertyOrder(5)]int ConnectionFailures
        );
}