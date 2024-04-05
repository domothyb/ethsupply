using Newtonsoft.Json;

namespace EthSupply.DataRepository;

public class DataObject
{
    [JsonProperty("lastSupplyAlert")]
    public long LastSupplyAlert { get; set; }
    
    [JsonProperty("lastSupplySeen")]
    public double LastSupplySeen { get; set; }
}