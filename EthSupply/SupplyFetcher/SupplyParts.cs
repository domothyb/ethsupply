using Newtonsoft.Json;

namespace EthSupply;

public class SupplyParts
{
    [JsonProperty("beaconBalancesSum")]
    public string BeaconBalancesSum { get; set; }
    
    [JsonProperty("beaconDepositsSum")]
    public string BeaconDepositsSum { get; set; }
    
    [JsonProperty("blockNumber")]
    public int BlockNumber { get; set; }
    
    [JsonProperty("ExecutionBalancesSum")]
    public string ExecutionBalancesSum { get; set;  }
}