using Newtonsoft.Json;

namespace EthSupply.DataRepository;

public class DataObject
{
    [JsonProperty("token")]
    public string Token { get; set; }
    
    [JsonProperty("channelId")]
    public long ChannelId { get; set; }
    
    [JsonProperty("lastSupplyAlert")]
    public double LastSupplyAlert { get; set; }
    
    [JsonProperty("lastSupplySeen")]
    public double LastSupplySeen { get; set; }
}