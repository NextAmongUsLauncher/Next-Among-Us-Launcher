using System.Text.Json.Serialization;

namespace NextAmongUsLauncher.Core.Base;

public sealed class Server
    (
        string type, 
        string name, 
        string pingServer, 
        string? targetServer, 
        int translateName, 
        List<Server.ServerInfo> serverInfos
        )
{
    [JsonPropertyName("$type"), JsonPropertyOrder(0)]
    public string Type { get; set; } = type;

    [JsonPropertyOrder(1)]
    public string Name { get; set; } = name;

    [JsonPropertyOrder(2)]
    public string PingServer { get; set; } = pingServer;

    [JsonPropertyName("Servers"), JsonPropertyOrder(3)]
    public List<ServerInfo> ServerInfos { get; set; } = serverInfos;

    [JsonPropertyOrder(4)]
    public string? TargetServer { get; set; } = targetServer;

    [JsonPropertyOrder(4)]
    public int TranslateName { get; set; } = translateName;


    public sealed record ServerInfo(
        [property: JsonPropertyOrder(0)]string Name, 
        [property: JsonPropertyOrder(1)]string Ip, 
        [property: JsonPropertyOrder(2)]ushort Port, 
        [property: JsonPropertyOrder(3)]bool UseDtls, 
        [property: JsonPropertyOrder(4)]int Players, 
        [property: JsonPropertyOrder(5)]int ConnectionFailures
        );
}