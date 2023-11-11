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

    public string Type { get; set; }
    
    public string Name { get; set; }
    
    public string PingServer { get; set; }
    
    public List<ServerInfo> ServerInfos { get; set; }
    
    public string? TargetServer { get; set; }
    
    public int TranslateName { get; set; }
    

    public sealed record ServerInfo(string Name, string Ip, ushort Port, bool UseDtls, int Players, int ConnectionFailures);
}