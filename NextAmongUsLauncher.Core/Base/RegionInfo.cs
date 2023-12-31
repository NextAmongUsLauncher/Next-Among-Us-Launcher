using System.Text.Json;
using System.Text.Json.Serialization;
using NextAmongUsLauncher.Core.Utils;

namespace NextAmongUsLauncher.Core.Base;

public class RegionInfo
{
    [JsonPropertyOrder(0)] public int CurrentRegionIdx { get; set; }
    
    [JsonPropertyOrder(1)]
    public List<Server>? Regions { get; set; }

    public override string ToString()
    {
        return JsonSerializer.Serialize(this, AmongUsServerSerialization._SerializerOptions);;
    }
}